namespace FileTasks;

public class Team
{
    public const int PlayersCount = 15;

    public string Name { get; }
    public IReadOnlyList<Player> Players { get; }
    public int TotalPoints { get; set; }

    public Team(string name, Random rnd)
    {
        Name = name;
        Players = Enumerable.Range(1, PlayersCount)
                            .Select(i => new Player($"{name} #{i}", rnd))
                            .ToList();
    }
}
