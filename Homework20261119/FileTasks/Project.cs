namespace FileTasks;

public class Project
{
    private readonly List<ProjectTask> _tasks = new();

    public string Description { get; }
    public DateTime Deadline { get; }
    public Person Customer { get; }
    public Person TeamLead { get; }
    public ProjectStatus Status { get; private set; }
    public IReadOnlyList<ProjectTask> Tasks => _tasks.AsReadOnly();

    public Project(string description, DateTime deadline, Person customer, Person teamLead)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание проекта не может быть пустым.", nameof(description));
        if (deadline <= DateTime.Now)
            throw new ArgumentException("Срок проекта должен быть в будущем.", nameof(deadline));

        Description = description;
        Deadline = deadline;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        TeamLead = teamLead ?? throw new ArgumentNullException(nameof(teamLead));
        Status = ProjectStatus.Project;
    }

    /// <summary>
    /// Создает задачу. Если расписание не указано, задача разовая = один отчет к сроку.
    /// </summary>
    public ProjectTask CreateTask(Person creator, string description, DateTime deadline,
        Person executor, ReportSchedule? schedule = null)
    {
        if (creator != TeamLead)
            throw new InvalidOperationException("Создавать задачи по проекту может только ответственный тимлид.");
        if (Status != ProjectStatus.Project)
            throw new InvalidOperationException("Создавать задачи можно только когда проект находится в статусе 'Проект'.");

        // Валидация в конструкторе задачи
        var task = new ProjectTask(this, description, deadline, TeamLead, executor, schedule ?? ReportSchedule.Once);
        _tasks.Add(task);

        string reporting = task.IsPeriodic
            ? $", отчетность: {task.Schedule}, отчетов: {task.ReportsRequired}"
            : "";
        Console.WriteLine($"[Создана задача] '{description}' -> Исполнитель: {executor.Name}{reporting}");
        return task;
    }

    /// <summary>
    /// Удаляет задачу. Удалить может её инициатор, пока задача в статусе
    /// 'Назначена' (в том числе отклоненная, без исполнителя) или 'В работе'.
    /// </summary>
    public void DeleteTask(Person initiator, ProjectTask task)
    {
        ArgumentNullException.ThrowIfNull(task);

        if (initiator != task.Initiator)
            throw new InvalidOperationException("Удалить задачу может только её инициатор.");
        if (!_tasks.Contains(task))
            throw new ArgumentException("Эта задача не принадлежит данному проекту.", nameof(task));
        if (task.Status != ProjectTaskStatus.Assigned && task.Status != ProjectTaskStatus.InProgress)
            throw new InvalidOperationException("Удалить можно только задачу в статусе 'Назначена' или 'В работе'.");

        _tasks.Remove(task);
        Console.WriteLine($"[Удаление] {initiator.Name} удалил задачу '{task.Description}'");
    }

    public void StartExecution()
    {
        if (Status != ProjectStatus.Project)
            throw new InvalidOperationException("Проект уже запущен или закрыт.");
        if (_tasks.Count == 0)
            throw new InvalidOperationException("Нельзя запустить проект без задач.");
        if (_tasks.Any(t => t.Executor == null))
            throw new InvalidOperationException("Нельзя запустить проект: есть задачи без исполнителя.");

        Status = ProjectStatus.Execution;
        Console.WriteLine($"\n--> Проект '{Description}' переведен в статус: 'Исполнение' <--\n");
    }

    /// <summary>
    /// Пытается закрыть проект. Возвращает true, если проект закрыт.
    /// Исключений не бросает: причина отказа выводится в консоль.
    /// </summary>
    public bool TryCloseProject()
    {
        if (Status != ProjectStatus.Execution)
        {
            Console.WriteLine("\n[Отказ закрытия] Закрыть можно только проект в статусе 'Исполнение'.");
            return false;
        }

        if (_tasks.Count == 0)
        {
            Console.WriteLine("\n[Отказ закрытия] В проекте нет задач.");
            return false;
        }

        int remaining = _tasks.Count(t => t.Status != ProjectTaskStatus.Completed);
        if (remaining > 0)
        {
            Console.WriteLine($"\n[Отказ закрытия] Проект нельзя закрыть. Не завершено задач: {remaining}");
            return false;
        }

        Status = ProjectStatus.Closed;
        Console.WriteLine($"\n--> Все задачи выполнены. Проект '{Description}' закрыт <--");
        return true;
    }
}
