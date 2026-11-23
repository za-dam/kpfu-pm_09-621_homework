namespace FileTasks;

public class BeachGame : GameBase
{
    public override string Name => "Пляж";
    protected override string Description => "Команда копает и переносит песок : важны сила и выносливость (5 лучших игроков)";
    protected override double CalculateScore(Team team) =>
        TopSum(team, p => p.Strength + p.Endurance, 5) + Luck(10);
}
