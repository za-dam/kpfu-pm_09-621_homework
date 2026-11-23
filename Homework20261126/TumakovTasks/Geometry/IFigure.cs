namespace TumakovTasks;

public interface IFigure
{
    void MoveHorizontal(double dx);
    void MoveVertical(double dy);
    void ChangeColor(FigureColor newColor);
    FigureVisibility StatusSurvey();
    void PrintInfo();
}
