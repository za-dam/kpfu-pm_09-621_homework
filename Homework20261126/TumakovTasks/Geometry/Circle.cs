namespace TumakovTasks;

public class Circle : Point
{
    public double Radius { get; private set; }

    public Circle(double x, double y, double radius, FigureColor color, FigureVisibility visibility) 
        : base(x, y, color, visibility)
    {
        Radius = radius;
    }

    public double CalculateArea() => Math.PI * Radius * Radius;

    public override void PrintInfo()
    {
        Console.Write("Окружность -> ");
        base.PrintInfo();
        Console.Write($", Радиус : {Radius}, Площадь : {CalculateArea():F2}");
    }
}
