namespace MyLib.File;

public struct User
{
    public string Name;
    public string City;
    public int Age;
    public string Pin;

    public User(string name, string city, int age, string pin)
    {
        Name = name;
        City = city;
        Age = age;
        Pin = pin;
    }
}