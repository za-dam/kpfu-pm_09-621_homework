namespace FileTasks;

public class ProjectTask
{
    private readonly List<TaskReport> _reports = new();

    public Project Project { get; }
    public string Description { get; }
    public DateTime Deadline { get; }
    public DateTime CreatedAt { get; }
    public Person Initiator { get; }
    public Person? Executor { get; private set; }
    public ProjectTaskStatus Status { get; private set; }

    // Расписание отчетности и рассчитанные по нему даты сдачи отчетов
    // У разовой задачи одна дата (срок задачи) и один отчет
    public ReportSchedule Schedule { get; }
    public IReadOnlyList<DateTime> DueDates { get; }
    public bool IsPeriodic => Schedule.Frequency != ReportFrequency.Once;

    // Все сданные отчеты : утвержденные и (если есть) один, который сейчас на проверке
    public IReadOnlyList<TaskReport> Reports => _reports.AsReadOnly();
    public int ReportsRequired => DueDates.Count;
    public int ReportsApproved { get; private set; }

    // Причина последнего возврата на доработку
    public string? ReviewComment { get; private set; }


    // internal : задачи создаются только через Project.CreateTask
    internal ProjectTask(Project project, string description, DateTime deadline,
        Person initiator, Person executor, ReportSchedule schedule)
    {
        Project = project ?? throw new ArgumentNullException(nameof(project));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание задачи не может быть пустым.", nameof(description));
        if (deadline < DateTime.Now)
            throw new ArgumentException("Срок задачи не может быть в прошлом.", nameof(deadline));
        if (deadline > project.Deadline)
            throw new ArgumentException("Срок задачи не может превышать срок проекта.", nameof(deadline));
        ArgumentNullException.ThrowIfNull(schedule);

        Description = description;
        Deadline = deadline;
        CreatedAt = DateTime.Now;
        Initiator = initiator ?? throw new ArgumentNullException(nameof(initiator));
        Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        Status = ProjectTaskStatus.Assigned;

        Schedule = schedule;
        DueDates = schedule.GetDueDates(CreatedAt, deadline);
        if (DueDates.Count == 0)
            throw new ArgumentException(
                "В срок задачи не попадает ни одной даты отчетности по выбранному расписанию.", nameof(schedule));
    }

    private void EnsureProjectInExecution()
    {
        if (Project.Status != ProjectStatus.Execution)
            throw new InvalidOperationException(
                "Работа с задачами возможна только когда проект находится в статусе 'Исполнение'.");
    }

    public void TakeInWork(Person employee)
    {
        EnsureProjectInExecution();
        ArgumentNullException.ThrowIfNull(employee);

        if (Executor != employee)
            throw new InvalidOperationException("Взять задачу в работу может только назначенный исполнитель.");
        if (Status != ProjectTaskStatus.Assigned)
            throw new InvalidOperationException("Взять в работу можно только задачу в статусе 'Назначена'.");

        Status = ProjectTaskStatus.InProgress;
        Console.WriteLine($"[В работе] {employee.Name} приступил к задаче: '{Description}'");
    }

    public void Delegate(Person currentExecutor, Person newExecutor)
    {
        EnsureProjectInExecution();
        ArgumentNullException.ThrowIfNull(currentExecutor);
        ArgumentNullException.ThrowIfNull(newExecutor);

        if (Executor != currentExecutor)
            throw new InvalidOperationException("Делегировать задачу может только текущий исполнитель.");
        if (Status != ProjectTaskStatus.Assigned && Status != ProjectTaskStatus.InProgress)
            throw new InvalidOperationException("Делегировать можно только задачу в статусе 'Назначена' или 'В работе'.");
        if (newExecutor == currentExecutor)
            throw new InvalidOperationException("Нельзя делегировать задачу самому себе.");

        // Уже утвержденные отчеты остаются в истории задачи, новый исполнитель продолжает работу
        Executor = newExecutor;
        Status = ProjectTaskStatus.Assigned;
        Console.WriteLine($"[Делегирование] {currentExecutor.Name} передал задачу '{Description}' -> {newExecutor.Name}");
    }

    public void Reject(Person employee)
    {
        EnsureProjectInExecution();
        ArgumentNullException.ThrowIfNull(employee);

        if (Executor != employee)
            throw new InvalidOperationException("Отклонить задачу может только текущий исполнитель.");
        if (Status != ProjectTaskStatus.Assigned)
            throw new InvalidOperationException("Отклонить можно только задачу в статусе 'Назначена'.");

        Console.WriteLine($"[Отклонение] {employee.Name} отклонил задачу: '{Description}'. Исполнитель снят.");
        Executor = null;
        Status = ProjectTaskStatus.Assigned;
    }

    public void Reassign(Person initiator, Person newExecutor)
    {
        EnsureProjectInExecution();
        ArgumentNullException.ThrowIfNull(newExecutor);

        if (Initiator != initiator)
            throw new InvalidOperationException("Переназначить задачу может только её инициатор.");
        if (Executor != null)
            throw new InvalidOperationException("Задача уже имеет исполнителя.");

        Executor = newExecutor;
        Status = ProjectTaskStatus.Assigned;
        Console.WriteLine($"[Переназначение] {initiator.Name} назначил задачу '{Description}' -> {newExecutor.Name}");
    }

    /// <summary>
    /// Сдает очередной отчет по задаче. completionDate - дата выполнения работы;
    /// если не указана, берется текущее время. Отчет за период нельзя сдать раньше,
    /// чем этот период начался (для первого отчета - с даты создания задачи,
    /// для следующих - с даты сдачи предыдущего отчета по расписанию).
    /// </summary>
    public void SubmitReport(Person employee, string reportText, DateTime? completionDate = null)
    {
        EnsureProjectInExecution();
        if (Executor != employee)
            throw new InvalidOperationException("Отправить отчет может только текущий исполнитель.");
        if (Status != ProjectTaskStatus.InProgress)
            throw new InvalidOperationException("Сдать отчет можно только по задаче, которая находится 'В работе'.");

        DateTime date = completionDate ?? DateTime.Now;
        DateTime periodStart = ReportsApproved == 0 ? CreatedAt.Date : DueDates[ReportsApproved - 1];
        if (date.Date < periodStart)
            throw new InvalidOperationException(
                $"Отчет за этот период можно сдать не раньше {periodStart:dd.MM.yyyy}.");

        var report = new TaskReport(reportText, employee, ReportsApproved + 1, DueDates[ReportsApproved], date);
        _reports.Add(report);
        Status = ProjectTaskStatus.InReview;

        string number = IsPeriodic ? $" №{report.Number}/{ReportsRequired} (за {report.DueDate:dd.MM.yyyy})" : "";
        Console.WriteLine($"[На проверке] {employee.Name} сдал отчет{number} по задаче '{Description}': \"{reportText}\"");
    }

    public void ApproveReport(Person initiator)
    {
        EnsureProjectInExecution();
        if (Initiator != initiator)
            throw new InvalidOperationException("Утвердить отчет может только инициатор задачи.");
        if (Status != ProjectTaskStatus.InReview)
            throw new InvalidOperationException("Задача не находится на проверке или отчет отсутствует.");

        ReportsApproved++;

        if (ReportsApproved >= ReportsRequired)
        {
            Status = ProjectTaskStatus.Completed;
            string progress = IsPeriodic ? $" (отчетов утверждено: {ReportsApproved}/{ReportsRequired})" : "";
            Console.WriteLine($"[Выполнена] {initiator.Name} утвердил отчет по задаче '{Description}'{progress}");
        }
        else
        {
            // Периодическая задача : отчет принят, но если нужны еще отчеты, то задача остается в работе
            Status = ProjectTaskStatus.InProgress;
            Console.WriteLine($"[Отчет принят] {initiator.Name} утвердил отчет {ReportsApproved}/{ReportsRequired} " +
                              $"по задаче '{Description}', задача остается в работе");
        }
    }

    public void ReturnForRevision(Person initiator, string comment)
    {
        EnsureProjectInExecution();
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Нужно указать причину возврата на доработку.", nameof(comment));
        if (Initiator != initiator)
            throw new InvalidOperationException("Вернуть отчет может только инициатор задачи.");
        if (Status != ProjectTaskStatus.InReview)
            throw new InvalidOperationException("Вернуть можно только задачу, находящуюся на проверке.");

        _reports.RemoveAt(_reports.Count - 1);
        ReviewComment = comment;
        Status = ProjectTaskStatus.InProgress;
        Console.WriteLine($"[На доработку] {initiator.Name} вернул задачу '{Description}' ({Executor?.Name}). Причина: {comment}");
    }
}
