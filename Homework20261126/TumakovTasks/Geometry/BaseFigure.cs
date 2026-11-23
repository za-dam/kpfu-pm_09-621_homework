namespace TumakovTasks;

public abstract class BaseFigure : IFigure
{
    public FigureColor Color { get; private set; }
    public FigureVisibility Visibility { get; private set; }

    protected BaseFigure(FigureColor color, FigureVisibility visibility)
    {
        Color = color;
        Visibility = visibility;
    }

    public abstract void MoveHorizontal(double dx);
    public abstract void MoveVertical(double dy);

    public void ChangeColor(FigureColor newColor) => Color = newColor;

    public FigureVisibility StatusSurvey() => Visibility;

    public virtual void PrintInfo()
    {
        Console.Write($"Цвет : {Color}, Видимость : {Visibility}");
    }
}