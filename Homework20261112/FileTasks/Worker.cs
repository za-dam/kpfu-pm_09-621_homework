namespace FileTasks;

public class Worker : Employee
{
    public Worker(string name, string title, TaskType department, Employee manager = null, Employee deputyFor = null) 
                : base(name, title, department, manager, deputyFor) { }
}