namespace FileTasks;

public class Task
{
    public string Title { get; }
    public TaskType Type { get; }

    public Task(string title, TaskType type)
    {
        Title = title;
        Type = type;
    }
}
