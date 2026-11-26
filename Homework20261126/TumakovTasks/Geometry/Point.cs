namespace TumakovTasks;

public class Point : FigureBase
{
    public double X { get; protected set; }
    public double Y { get; protected set; }

    public Point(double x, double y, FigureColor color, FigureVisibility visibility) 
        : base(color, visibility)
    {
        X = x;
        Y = y;
    }

    public override void MoveHorizontal(double dx) => X += dx;
    public override void MoveVertical(double dy) => Y += dy;

    public override void PrintInfo()
    {
        Console.Write("Точка -> ");
        base.PrintInfo();
        Console.Write($", Координаты : ({X}, {Y})");
    }
}
