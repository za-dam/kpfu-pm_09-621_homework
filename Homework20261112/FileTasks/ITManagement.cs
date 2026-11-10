namespace FileTasks;

public class ITManagement : Employee
{
    public ITManagement(string name, string title, TaskType department, Employee manager = null, Employee deputyFor = null) 
                        : base(name, title, department, manager, deputyFor) { }
}