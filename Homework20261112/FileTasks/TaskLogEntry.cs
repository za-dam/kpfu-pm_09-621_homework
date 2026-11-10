namespace FileTasks;

public class TaskLogEntry
{
    public DateTime Timestamp { get; } // Дата и время постановки задачи
    public Task Task { get; } // Ссылка на саму задачу
    public Employee Sender { get; } // Кто назначал задачу
    public Employee Receiver { get; } // Кому назначали задачу
    public TaskStatus FinalStatus { get; } // Итоговый статус
    public string Comment { get; } // Причина отказа или комментарий

    public TaskLogEntry(Task task, Employee sender, Employee receiver, TaskStatus finalStatus, string comment = null)
    {
        Timestamp = DateTime.Now;
        Task = task;
        Sender = sender;
        Receiver = receiver;
        FinalStatus = finalStatus;
        Comment = comment ?? string.Empty;
    }
}