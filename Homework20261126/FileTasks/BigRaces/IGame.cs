namespace FileTasks;

/// <summary>
/// Контракт любой игры. Шоу «Большие гонки» знает только про этот интерфейс, поэтому новые игры добавляются без изменения шоу
/// </summary>
public interface IGame
{
    string Name { get; }
    void Play(IReadOnlyList<Team> teams);
}
