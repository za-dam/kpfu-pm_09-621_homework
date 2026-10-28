namespace FileTasks;


public class ScratchTicket : LotteryTicket
{
    public const decimal DefaultPrice = 50m;
    private readonly decimal _hiddenPrize;

    public bool IsScratched { get; private set; }
    public DateTime? ScratchTime { get; private set; }  

    public ScratchTicket(Guid ownerId, decimal hiddenPrize)
        : base(ownerId, DefaultPrice)
    {
        if (hiddenPrize < 0)
        {
            throw new ArgumentException("Приз не может быть отрицательным.");
        }

        _hiddenPrize = hiddenPrize;
    }

    public ScratchTicket(Guid ownerId)
        : this(ownerId, GenerateHiddenPrize())
    {
    }

    private static decimal GenerateHiddenPrize()
    {
        int roll = Random.Shared.Next(100);

        return roll switch
        {
            < 70 => 0m,
            < 90 => 50m,
            < 98 => 200m,
            _    => 1000m
        };
    }

    public void Scratch()
    {
        if (IsScratched)
        {
            Console.WriteLine($"Билет {TicketId}: слой уже стёрт");
            return;
        }

        IsScratched = true;
        ScratchTime = DateTime.Now;
        SetResult(_hiddenPrize);

        if (PrizeAmount > 0)
        {
            Console.WriteLine($"Билет {TicketId}: слой стёрт, выигрыш {PrizeAmount:N2} руб.!");
        }
        else
        {
            Console.WriteLine($"Билет {TicketId}: слой стёрт, увы, пусто");
        }
    }

    public override void CheckResult(List<int> winningNumbers)
    {
        Scratch();
    }

    private string GetScratchText()
    {
        if (!IsScratched)
        {
            return "слой не стёрт";
        }

        return $"{GetStatusText(Status)}, приз {PrizeAmount:N2} руб.";
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"""
        [Скретч] {TicketId} |
        Владелец : {OwnerId.ToString("N").Substring(0, 8).ToUpper()} | 
        {GetScratchText()}
        """);
    }

    public override void ShowRules()
    {
        Console.WriteLine($"[Скретч-билет, {DefaultPrice} руб.] Числа выбирать не нужно : стираете слой и сразу видите результат");
        Console.WriteLine("  Приз : от 0 до 1000 руб., чаще всего - пусто");
    }
}
