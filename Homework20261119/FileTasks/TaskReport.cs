namespace FileTasks;

public class TaskReport
{
    public string Text { get; }
    public DateTime CompletionDate { get; }
    public Person Executor { get; }

    // Порядковый номер отчета по задаче (для разовой задачи всегда 1)
    public int Number { get; }

    // Дата, к которой нужно было сдать этот отчет по расписанию
    public DateTime DueDate { get; }

    public TaskReport(string text, Person executor, int number, DateTime dueDate, DateTime completionDate)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Текст отчета не может быть пустым.", nameof(text));
        if (number < 1)
            throw new ArgumentOutOfRangeException(nameof(number), "Номер отчета начинается с 1.");

        Text = text;
        Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        Number = number;
        DueDate = dueDate;
        CompletionDate = completionDate;
    }
}
