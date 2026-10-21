namespace FileTasks;

public struct Student
{
    public Guid Id;
    public string Surname;
    public string Name;
    public DateTime BirthYear;
    public string Exam;
    public byte Score;
 
    public Student(string surname, string name, DateTime birthYear, string exam, byte score)
    {
        Id = Guid.NewGuid(); // каждый новый студент сразу получает уникальный Id
        Surname = surname;
        Name = name;
        BirthYear = birthYear;
        Exam = exam;
        Score = score;
    }
 
    public string GetInfo()
    {
        return $"Id: {Id} | {Surname} {Name}, дата рождения : {BirthYear:dd.MM.yyyy}, экзамен: {Exam}, баллы: {Score}";
    }
}