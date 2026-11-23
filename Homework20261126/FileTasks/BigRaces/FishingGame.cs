namespace FileTasks;

public class FishingGame : GameBase
{
    public override string Name => "Рыбалка";
    protected override string Description => "Нужно поймать больше рыбы : важны сообразительность и ловкость (3 лучших игрока)";
    protected override double CalculateScore(Team team) =>
        TopSum(team, p => p.Intellect + p.Agility, 3) + Luck(12);
}
