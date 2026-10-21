namespace FileTasks;

public class Grandmother
{
    public string Name;
    public DateTime BirthDate;
    public List<string> Diseases;  
    public List<string> Medicines;
    public Grandmother(string name, DateTime birthDate, List<string> diseases, List<string> medicines)
    {
        Name = name;
        BirthDate = birthDate;
        Diseases = diseases;
        Medicines = medicines;
    }
}