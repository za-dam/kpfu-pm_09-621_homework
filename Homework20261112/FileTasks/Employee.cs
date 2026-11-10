namespace FileTasks;

public abstract class Employee
{
    public string Name { get; }
    public string Title { get; }
    public TaskType Department { get; }
    public Employee Manager { get; }
    public Employee DeputyFor { get; }  // Ссылка на сотрудника, чьим заместителем является данный человек

    protected Employee(string name, string title, TaskType department, Employee manager = null, Employee deputyFor = null)
    {
        Name = name;
        Title = title;
        Department = department;
        Manager = manager;
        DeputyFor = deputyFor;
    }

    public virtual bool CanAcceptTaskFrom(Employee sender)
    {
        if (sender == null)
        {
            return false;
        }

        if (sender == Manager || sender.DeputyFor == Manager)
        {
            return true;
        }

        return false;
    }
}