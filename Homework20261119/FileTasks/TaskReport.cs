namespace FileTasks;

public class TaskReport
{
    public string Text { get; }
    public DateTime CompletionDate { get; }
    public Person Executor { get; }

    public TaskReport(string text, Person executor)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Текст отчета не может быть пустым.");

        Text = text;
        Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        CompletionDate = DateTime.Now;
    }
}
