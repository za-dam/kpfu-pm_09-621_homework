namespace FileTasks;

public class Player
{
    public string Name { get; }
    public int Strength { get; }   // сила
    public int Agility { get; }    // ловкость
    public int Endurance { get; }  // выносливость
    public int Intellect { get; }  // сообразительность

    public Player(string name, Random rnd)
    {
        Name = name;
        Strength = rnd.Next(3, 11);
        Agility = rnd.Next(3, 11);
        Endurance = rnd.Next(3, 11);
        Intellect = rnd.Next(3, 11);
    }
}
