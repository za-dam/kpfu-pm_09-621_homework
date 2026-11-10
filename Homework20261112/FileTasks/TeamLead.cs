namespace FileTasks;

 public class TeamLead : Employee
{
    public TeamLead(string name, string title, TaskType department, Employee manager = null, Employee deputyFor = null) 
                    : base(name, title, department, manager, deputyFor) { }

}