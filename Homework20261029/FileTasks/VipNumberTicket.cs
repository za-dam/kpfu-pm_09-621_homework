namespace FileTasks;


public class VipNumberTicket : NumberTicket
{
    public const decimal VipPrice = 300m;
    public const int BonusMultiplier = 2;

    public int BonusNumber { get; }
    public bool BonusHit { get; private set; }


    public VipNumberTicket(Guid ownerId, IEnumerable<int> numbers, int bonusNumber)
        : base(ownerId, VipPrice, numbers)
    {
        if (!IsValidBonus(bonusNumber, ChosenNumbers))
        {
            throw new ArgumentException($"Бонусное число должно быть от 1 до {MaxNumber} и не совпадать с выбранными");
        }

        BonusNumber = bonusNumber;
    }

    public VipNumberTicket(Guid ownerId)
        : base(ownerId, VipPrice, GenerateRandomNumbers())
    {
        BonusNumber = GenerateBonusNumber(ChosenNumbers);
    }

    public static bool IsValidBonus(int bonus, IEnumerable<int> chosen)
    {
        if (bonus < 1 || bonus > MaxNumber)
        {
            return false;
        }

        foreach (int number in chosen)
        {
            if (number == bonus)
            {
                return false;
            }
        }

        return true;
    }

    private static int GenerateBonusNumber(IEnumerable<int> chosen)
    {
        while (true)
        {
            int bonus = Random.Shared.Next(1, MaxNumber + 1);

            if (IsValidBonus(bonus, chosen))
            {
                return bonus;
            }
        }
    }

    public override void CheckResult(List<int> winningNumbers)
    {
        base.CheckResult(winningNumbers);

        BonusHit = winningNumbers.Contains(BonusNumber);

        if (BonusHit && PrizeAmount > 0)
        {
            PrizeAmount *= BonusMultiplier;
        }
    }

    public override void DisplayInfo()
    {
        string bonusText = BonusHit ? ", бонус выпал (приз x2)" : string.Empty;

        Console.WriteLine($"""
        [VIP] {TicketId} | 
        Владелец : {OwnerId.ToString("N").Substring(0, 8).ToUpper()} |
        Числа : {string.Join("-", ChosenNumbers)} + бонус {BonusNumber} |
        Совпало : {Matches}{bonusText} | {GetStatusText(Status)} | Приз : {PrizeAmount:N2} руб.
        """);
    }

    public override void ShowRules()
    {
        Console.WriteLine($"[VIP-билет, {VipPrice} руб.] Как числовой, плюс бонусное число (не из выбранных)");
        Console.WriteLine($"  Если бонусное число выпало в тираже, обычный приз умножается на {BonusMultiplier}");
    }
}
