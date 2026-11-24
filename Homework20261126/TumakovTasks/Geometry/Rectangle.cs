namespace TumakovTasks;

public class Rectangle : Point
{
    public double Length { get; private set; }
    public double Width { get; private set; }

    public Rectangle(double x, double y, double width, double length, FigureColor color, FigureVisibility visibility) 
        : base(x, y, color, visibility)
    {
        Width = width;
        Length = length;
    }

    public double CalculateRectangleArea() => Length * Width;

    public override void PrintInfo()
    {
        Console.Write("Прямоугольник -> ");
        base.PrintInfo();
        Console.Write($", Длина : {Length}, Ширина : {Width}, Площадь : {CalculateRectangleArea():F2}");
    }
}
