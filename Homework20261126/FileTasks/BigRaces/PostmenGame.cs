namespace FileTasks;

public class PostmenGame : GameBase
{
    public override string Name => "Почтальоны";
    protected override string Description => "Доставка писем на время : важны ловкость и выносливость (5 лучших игроков)";
    protected override double CalculateScore(Team team) =>
        TopSum(team, p => p.Agility + p.Endurance, 5) + Luck(10);
}
