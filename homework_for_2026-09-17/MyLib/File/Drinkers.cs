namespace MyLib.File;

public enum AlcoCategory
{
    Alcoholic,  
    Drinker, 
    HolidayOnly,
    Abstainer 
}

public struct Drink
{
    public string Name; 
    public double AlcoholPercent;
    public Drink(string name, double alcoholPercent)
    {
        Name = name;
        AlcoholPercent = alcoholPercent;
    }
}

public struct Student
{
    public string LastName;
    public string FirstName;
    public int Id;
    public DateTime BirthDate;
    public AlcoCategory Category;
    public double VolumeMl;
    public Drink Drink;

    public Student(string lastName, string firstName, int id,
                   DateTime birthDate, AlcoCategory category,
                   double volumeMl, Drink drink)
    {
        LastName = lastName;
        FirstName = firstName;
        Id = id;
        BirthDate = birthDate;
        Category = category;
        VolumeMl = volumeMl;
        Drink = drink;
    }
    public double PureAlcoholMl()
    {
        return VolumeMl * Drink.AlcoholPercent / 100.0;
    }
}