namespace FileTasks;

public class Person
{
    public string Name { get; }
    public string Role { get; }

    public Person(string name, string role)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя сотрудника не может быть пустым.");
        
        Name = name;
        Role = role;
    }

    public override string ToString() => $"{Name} ({Role})";
}
