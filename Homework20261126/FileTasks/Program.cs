namespace FileTasks;

internal static class Program
{
    private static void Main()
    {
        var rnd = new Random();

        var show = new BigRaces();

        show.AddTeam(new Team("Россия", rnd));
        show.AddTeam(new Team("Франция", rnd));
        show.AddTeam(new Team("Китай", rnd));
        show.AddTeam(new Team("Казахстан", rnd));

        show.AddGame(new BeachGame());
        show.AddGame(new MousetrapGame());
        show.AddGame(new SeaGame());
        show.AddGame(new FishingGame());
        show.AddGame(new PostmenGame());
        show.AddGame(new SlideGame());

        show.AddGame(new MazeGame());

        show.Run();

        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}