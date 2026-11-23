namespace FileTasks;

public class BigRaces
{
    private readonly List<Team> _teams = new List<Team>();
    private readonly List<IGame> _games = new List<IGame>();

    public void AddTeam(Team team) => _teams.Add(team);
    public void AddGame(IGame game) => _games.Add(game);

    public void Run()
    {
        Console.WriteLine("Добро пожаловать на шоу «Большие гонки»!");
        Console.WriteLine($"Участвуют команды ({_teams.Count}) : {string.Join(", ", _teams.Select(t => t.Name))}");
        Console.WriteLine($"В каждой команде по {Team.PlayersCount} человек. Испытаний : {_games.Count}.");

        foreach (var game in _games)
            game.Play(_teams);

        PrintFinalResults();
    }

    private void PrintFinalResults()
    {
        Console.WriteLine();
        Console.WriteLine("--> Итоги <--");

        var final = _teams.OrderByDescending(t => t.TotalPoints).ToList();

        for (int i = 0; i < final.Count; i++)
            Console.WriteLine($"{i + 1}. {final[i].Name,-10} - {final[i].TotalPoints} очк.");

        Console.WriteLine();
        Console.WriteLine($"Победитель «Больших гонок» : {final[0].Name}!");
    }
}
