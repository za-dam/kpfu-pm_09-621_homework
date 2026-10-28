namespace FileTasks;


// Абстрактный базовый класс
public abstract class LotteryTicket
{
    private static int _lastTicketNumber = 0;
    private Guid _ownerId;

    public string TicketId { get; }
    public decimal Price { get; }

    public Guid OwnerId => _ownerId;

    public TicketStatus Status { get; protected set; } = TicketStatus.Purchased;
    public decimal PrizeAmount { get; protected set; }

    public virtual bool IsJackpotWinner
    {
        get { return false; }
    }

    protected LotteryTicket(Guid ownerId, decimal price)
    {
        _ownerId = ownerId;
        Price = price;
        TicketId = GenerateTicketId();
    }

    // Для классов наследников
    // Проверить билет по выпавшим числам
    public abstract void CheckResult(List<int> winningNumbers);

    // Вывести информацию о билете
    public abstract void DisplayInfo();

    // Рассказать правила этого типа билета
    public abstract void ShowRules();

    // Общие методы для всех билетов
    public bool IsWinner()
    {
        return Status == TicketStatus.Won;
    }

    public string GetShortInfo()
    {
        return $"{TicketId} ({OwnerId.ToString("N").Substring(0, 8).ToUpper()})";
    }

    // Передать билет другому человеку
    public void ChangeOwner(Guid newOwnerId)
    {
        Guid oldOwnerId = OwnerId;
        _ownerId = newOwnerId;
        Console.WriteLine($"""
        Билет {TicketId} передан : {oldOwnerId.ToString("N").Substring(0, 8).ToUpper()} 
        -> {newOwnerId.ToString("N").Substring(0, 8).ToUpper()}
        """);
    }

    // Выдать долю джекпота
    // Работает только для билета, который действительно угадал всё
    public void AwardJackpot(decimal amount)
    {
        if (!IsJackpotWinner)
        {
            return;
        }

        PrizeAmount = amount;
        Status = TicketStatus.Won;
    }

    // Общая логика итога : есть приз - выиграл, нет - проиграл
    // Нужно наследникам
    protected void SetResult(decimal prize)
    {
        PrizeAmount = prize;
        Status = prize > 0 ? TicketStatus.Won : TicketStatus.Lost;
    }

    protected static string GetStatusText(TicketStatus status)
    {
        switch (status)
        {
            case TicketStatus.Purchased: return "ждёт тиража";
            case TicketStatus.Won: return "ВЫИГРАЛ";
            case TicketStatus.Lost: return "проиграл";
            default: return "неизвестно";
        }
    }

    // T-0001, T-0002 и т.д. 
    private static string GenerateTicketId()
    {
        _lastTicketNumber++;
        return $"T-{_lastTicketNumber:D4}";
    }
}