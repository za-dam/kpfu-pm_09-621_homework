namespace FileTasks;

public class ProjectTask
{
    public string Description { get; }
    public DateTime Deadline { get; }
    public Person Initiator { get; }
    public Person? Executor { get; private set; }
    public TaskStatus Status { get; private set; }
    public TaskReport? Report { get; private set; }

    public ProjectTask(string description, DateTime deadline, Person initiator, Person executor)
    {
        Description = description;
        Deadline = deadline;
        Initiator = initiator ?? throw new ArgumentNullException(nameof(initiator));
        Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        Status = TaskStatus.Assigned;
    }

    public void TakeInWork(Person employee)
    {
        if (Executor != employee)
            throw new InvalidOperationException("Взять задачу в работу может только назначенный исполнитель.");
        if (Status != TaskStatus.Assigned)
            throw new InvalidOperationException("Взять в работу можно только задачу в статусе 'Назначена'.");

        Status = TaskStatus.InProgress;
        Console.WriteLine($"[В работе] {employee.Name} приступил к задаче: '{Description}'");
    }

    public void Delegate(Person currentExecutor, Person newExecutor)
    {
        if (Executor != currentExecutor)
            throw new InvalidOperationException("Делегировать задачу может только текущий исполнитель.");
        if (Status != TaskStatus.Assigned && Status != TaskStatus.InProgress)
            throw new InvalidOperationException("Невозможно делегировать задачу на текущем этапе.");

        Executor = newExecutor ?? throw new ArgumentNullException(nameof(newExecutor));
        Status = TaskStatus.Assigned;
        Console.WriteLine($"[Делегирование] {currentExecutor.Name} передал задачу '{Description}' -> {newExecutor.Name}");
    }

    public void Reject(Person employee)
    {
        if (Executor != employee)
            throw new InvalidOperationException("Отклонить задачу может только текущий исполнитель.");

        Console.WriteLine($"[Отклонение] {employee.Name} отклонил задачу: '{Description}'. Исполнитель снят.");
        Executor = null;
        Status = TaskStatus.Assigned;
    }

    public void Reassign(Person initiator, Person newExecutor)
    {
        if (Initiator != initiator)
            throw new InvalidOperationException("Переназначить задачу может только её инициатор.");
        if (Executor != null)
            throw new InvalidOperationException("Задача уже имеет исполнителя.");

        Executor = newExecutor ?? throw new ArgumentNullException(nameof(newExecutor));
        Status = TaskStatus.Assigned;
        Console.WriteLine($"[Переназначение] {initiator.Name} назначил задачу '{Description}' -> {newExecutor.Name}");
    }

    public void SubmitReport(Person employee, string reportText)
    {
        if (Executor != employee)
            throw new InvalidOperationException("Отправить отчет может только текущий исполнитель.");
        if (Status != TaskStatus.InProgress)
            throw new InvalidOperationException("Сдать отчет можно только по задаче, которая находится 'В работе'.");

        Report = new TaskReport(reportText, employee);
        Status = TaskStatus.InReview;
        Console.WriteLine($"[На проверке] {employee.Name} сдал отчет по задаче '{Description}': \"{reportText}\"");
    }

    public void ApproveReport(Person initiator)
    {
        if (Initiator != initiator)
            throw new InvalidOperationException("Утвердить отчет может только инициатор задачи.");
        if (Status != TaskStatus.InReview || Report == null)
            throw new InvalidOperationException("Задача не находится на проверке или отчет отсутствует.");

        Status = TaskStatus.Completed;
        Console.WriteLine($"[Выполнена] {initiator.Name} утвердил отчет по задаче '{Description}'");
    }

    public void ReturnForRevision(Person initiator, string comment)
    {
        if (Initiator != initiator)
            throw new InvalidOperationException("Вернуть отчет может только инициатор задачи.");
        if (Status != TaskStatus.InReview)
            throw new InvalidOperationException("Вернуть можно только задачу, находящуюся на проверке.");

        Report = null;
        Status = TaskStatus.InProgress;
        Console.WriteLine($"[На доработку] {initiator.Name} вернул задачу '{Description}' ({Executor?.Name}). Причина: {comment}");
    }
}
