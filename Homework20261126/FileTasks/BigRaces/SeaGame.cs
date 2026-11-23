namespace FileTasks;

public class SeaGame : GameBase
{
    public override string Name => "Море";
    protected override string Description => "Заплыв и эстафета на воде : важны выносливость и сила (6 лучших игроков)";
    protected override double CalculateScore(Team team) =>
        TopSum(team, p => p.Endurance * 2 + p.Strength, 6) / 2 + Luck(10);
}
