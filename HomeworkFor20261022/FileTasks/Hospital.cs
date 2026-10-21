namespace FileTasks;

public class Hospital
{
    public string Name;
    public List<string> TreatedDiseases;
    public int Capacity;
    public List<Grandmother> Patients = new List<Grandmother>();

    public Hospital(string name, List<string> treatedDiseases, int capacity)
    {
        Name = name;
        TreatedDiseases = treatedDiseases;
        Capacity = capacity;
    }

    public bool HasFreePlace()
    {
        return Patients.Count < Capacity;
    }

    public double FillPercent()
    {
        return Patients.Count * 100.0 / Capacity;
    }
}