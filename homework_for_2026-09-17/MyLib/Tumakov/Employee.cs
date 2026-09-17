namespace MyLib.Tumakov;

public struct Employee
{
    public string Name;
    public University University;

    public Employee(string name, University univer)
    {
        Name = name;
        University = univer;
    }
}

public enum University
{
    KGU,
    KAI, 
    KHTI,
    Unknown
}