namespace FileTasks;

public class SlideGame : GameBase
{
    public override string Name => "Горка";
    protected override string Description => "Забраться на скользкую горку : важны сила и ловкость (4 лучших игрока)";
    protected override double CalculateScore(Team team) =>
        TopSum(team, p => p.Strength + p.Agility, 4) + Luck(12);
}
