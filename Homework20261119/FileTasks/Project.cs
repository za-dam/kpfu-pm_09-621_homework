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
        Description = description;
        Deadline = deadline;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        TeamLead = teamLead ?? throw new ArgumentNullException(nameof(teamLead));
        Status = ProjectStatus.Project;
    }

    public ProjectTask CreateTask(Person creator, string description, DateTime deadline, Person executor)
    {
        if (creator != TeamLead)
            throw new InvalidOperationException("Создавать задачи по проекту может только ответственный тимлид.");
        if (Status != ProjectStatus.Project)
            throw new InvalidOperationException("Создавать задачи можно только когда проект находится в статусе 'Проект'.");

        var task = new ProjectTask(description, deadline, TeamLead, executor);
        _tasks.Add(task);
        Console.WriteLine($"[Создана задача] '{description}' -> Исполнитель: {executor.Name}");
        return task;
    }

    public void DeleteRejectedTask(Person initiator, ProjectTask task)
    {
        if (initiator != TeamLead)
            throw new InvalidOperationException("Удалить задачу может только тимлид.");
        if (task.Executor != null)
            throw new InvalidOperationException("Удалить можно только отклоненную задачу без исполнителя.");

        _tasks.Remove(task);
        Console.WriteLine($"[Удаление] Тимлид {TeamLead.Name} удалил отклоненную задачу '{task.Description}'");
    }

    public void StartExecution()
    {
        if (Status != ProjectStatus.Project)
            throw new InvalidOperationException("Проект уже запущен или закрыт.");
        if (_tasks.Count == 0)
            throw new InvalidOperationException("Нельзя запустить проект без задач.");

        Status = ProjectStatus.Execution;
        Console.WriteLine($"\n--> Проект '{Description}' переведен в статус: 'Исполнение' <--\n");
    }

    public void TryCloseProject()
    {
        if (Status != ProjectStatus.Execution)
            throw new InvalidOperationException("Закрыть можно только проект в статусе 'Исполнение'.");

        if (_tasks.All(t => t.Status == TaskStatus.Completed))
        {
            Status = ProjectStatus.Closed;
            Console.WriteLine($"\n--> Все задачи выполнены. Проект '{Description}' закрыт <--");
        }
        else
        {
            int remaining = _tasks.Count(t => t.Status != TaskStatus.Completed);
            Console.WriteLine($"\n[Отказ закрытия] Проект нельзя закрыть. Не завершено задач: {remaining}");
        }
    }
}
