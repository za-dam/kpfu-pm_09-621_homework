namespace FileTasks;

public class MousetrapGame : GameBase
{
    public override string Name => "Мышеловка";
    protected override string Description => "Нужно проскочить между захлопывающимися ловушками : важны ловкость (4 лучших игрока) и удача";
    protected override double CalculateScore(Team team) =>
        TopSum(team, p => p.Agility, 4) + Luck(15);
}
