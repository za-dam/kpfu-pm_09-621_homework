namespace FileTasks;

public class TopManagement : Employee
{
    public TopManagement(string name, string title, TaskType department, Employee manager = null, Employee deputyFor = null) 
                        : base(name, title, department, manager, deputyFor) { }
}