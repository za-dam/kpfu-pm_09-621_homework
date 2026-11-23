namespace FileTasks;

public class MazeGame : GameBase
{
    public override string Name => "Лабиринт (авторская игра)";
    protected override string Description => "Команда ищет выход из лабиринта : важны сообразительность (3 лучших) и ловкость (2 лучших)";
    protected override double CalculateScore(Team team) =>
        TopSum(team, p => p.Intellect, 3) * 1.5 + TopSum(team, p => p.Agility, 2) + Luck(8);
}
