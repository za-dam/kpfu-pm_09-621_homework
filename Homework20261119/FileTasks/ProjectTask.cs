namespace FileTasks;

public class ProjectTask
{
    public Project Project { get; }
    public string Description { get; }
    public DateTime Deadline { get; }
    public Person Initiator { get; }
    public Person? Executor { get; private set; }
    public ProjectTaskStatus Status { get; private set; }

    // По условию : одна задача = один отчёт
    // При возврате на доработку отчёт обнуляется, а причина возврата сохраняется в ReviewComment
    public TaskReport? Report { get; private set; }
    public string? ReviewComment { get; private set; }

    // internal : задачи создаются только через Project.CreateTask
    internal ProjectTask(Project project, string description, DateTime deadline, Person initiator, Person executor)
    {
        Project = project ?? throw new ArgumentNullException(nameof(project));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание задачи не может быть пустым.", nameof(description));
        if (deadline < DateTime.Now)
            throw new ArgumentException("Срок задачи не может быть в прошлом.", nameof(deadline));
        if (deadline > project.Deadline)
            throw new ArgumentException("Срок задачи не может превышать срок проекта.", nameof(deadline));

        Description = description;
        Deadline = deadline;
        Initiator = initiator ?? throw new ArgumentNullException(nameof(initiator));
        Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        Status = ProjectTaskStatus.Assigned;
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
        if (Status != ProjectTaskStatus.Assigned)
            throw new InvalidOperationException("Делегировать можно только задачу в статусе 'Назначена'.");
        if (newExecutor == currentExecutor)
            throw new InvalidOperationException("Нельзя делегировать задачу самому себе.");

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

    public void SubmitReport(Person employee, string reportText)
    {
        EnsureProjectInExecution();
        if (Executor != employee)
            throw new InvalidOperationException("Отправить отчет может только текущий исполнитель.");
        if (Status != ProjectTaskStatus.InProgress)
            throw new InvalidOperationException("Сдать отчет можно только по задаче, которая находится 'В работе'.");

        Report = new TaskReport(reportText, employee);
        Status = ProjectTaskStatus.InReview;
        Console.WriteLine($"[На проверке] {employee.Name} сдал отчет по задаче '{Description}': \"{reportText}\"");
    }

    public void ApproveReport(Person initiator)
    {
        EnsureProjectInExecution();
        if (Initiator != initiator)
            throw new InvalidOperationException("Утвердить отчет может только инициатор задачи.");
        if (Status != ProjectTaskStatus.InReview || Report == null)
            throw new InvalidOperationException("Задача не находится на проверке или отчет отсутствует.");

        Status = ProjectTaskStatus.Completed;
        Console.WriteLine($"[Выполнена] {initiator.Name} утвердил отчет по задаче '{Description}'");
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

        Report = null;
        ReviewComment = comment;
        Status = ProjectTaskStatus.InProgress;
        Console.WriteLine($"[На доработку] {initiator.Name} вернул задачу '{Description}' ({Executor?.Name}). Причина: {comment}");
    }
}
