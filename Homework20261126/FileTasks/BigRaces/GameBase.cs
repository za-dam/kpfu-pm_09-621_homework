namespace FileTasks;

public abstract class GameBase : IGame
{
    private static readonly int[] PointsByPlace = { 4, 3, 2, 1 };
    protected static readonly Random Rnd = new Random();

    public abstract string Name { get; }
    protected abstract string Description { get; }

    protected abstract double CalculateScore(Team team);

    public void Play(IReadOnlyList<Team> teams)
    {
        Console.WriteLine();
        Console.WriteLine($"-> Игра : {Name} <-");
        Console.WriteLine(Description);

        var results = teams
            .Select(t => new { Team = t, Score = CalculateScore(t) })
            .OrderByDescending(r => r.Score)
            .ToList();

        for (int i = 0; i < results.Count; i++)
        {
            int points = i < PointsByPlace.Length ? PointsByPlace[i] : 0;
            results[i].Team.TotalPoints += points;
            Console.WriteLine($" {i + 1} место : {results[i].Team.Name,-10} результат {results[i].Score,6:F1}  (+{points} очк.)");
        }
    }

    protected static double TopSum(Team team, Func<Player, int> selector, int count) =>
        team.Players.Select(selector).OrderByDescending(x => x).Take(count).Sum();

    protected static double Luck(double max) => Rnd.NextDouble() * max;
}
