namespace MyLib.DataTypes;

public struct Candidate
{
    public string Name;
    public int Age;
    public double Height;
    public double Weight;
    public string FavoriteMovie;

    public Candidate(string name, int age, double height, double weight, string favoriteMovie)
    {
        Name = name;
        Age = age;
        Height = height;
        Weight = weight;
        FavoriteMovie = favoriteMovie;
    }
}