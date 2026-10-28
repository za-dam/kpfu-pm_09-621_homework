namespace FileTasks;


// 6 из 49 к примеру
public class NumberTicket : LotteryTicket
{
    public const int NumbersCount = 6; 
    public const int MaxNumber = 49;
    public const decimal DefaultPrice = 100m;

    public IReadOnlyList<int> ChosenNumbers { get; }

    public int Matches { get; private set; }

    public override bool IsJackpotWinner
    {
        get { return Matches == NumbersCount; }
    }

    public NumberTicket(Guid ownerId, IEnumerable<int> numbers)
        : this(ownerId, DefaultPrice, numbers)
    {
    }

    public NumberTicket(Guid ownerId)
        : this(ownerId, DefaultPrice, GenerateRandomNumbers())
    {
    }

    protected NumberTicket(Guid ownerId, decimal price, IEnumerable<int> numbers)
        : base(ownerId, price)
    {
        List<int> list = new List<int>(numbers);

        if (!IsValidSet(list, out string error))
        {
            throw new ArgumentException(error);
        }

        list.Sort();
        ChosenNumbers = list.AsReadOnly();
    }

    public static bool IsValidSet(List<int> numbers, out string error)
    {
        if (numbers.Count != NumbersCount)
        {
            error = $"Нужно ровно {NumbersCount} чисел, а передано {numbers.Count}";
            return false;
        }

        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] < 1 || numbers[i] > MaxNumber)
            {
                error = $"Число {numbers[i]} вне диапазона от 1 до {MaxNumber}";
                return false;
            }

            for (int j = i + 1; j < numbers.Count; j++)
            {
                if (numbers[i] == numbers[j])
                {
                    error = $"Число {numbers[i]} повторяется";
                    return false;
                }
            }
        }

        error = string.Empty;
        return true;
    }

    // Случайный набор из 6 разных чисел (используется и для быстрого выбора, и для тиража)
    public static List<int> GenerateRandomNumbers()
    {
        List<int> numbers = new List<int>();

        while (numbers.Count < NumbersCount)
        {
            int number = Random.Shared.Next(1, MaxNumber + 1);

            if (!numbers.Contains(number))
            {
                numbers.Add(number);
            }
        }

        numbers.Sort();
        return numbers;
    }

    // Таблица призов (6 совпадений - джекпот, его выдаёт аппарат)
    public static decimal GetPrizeForMatches(int matches)
    {
        switch (matches)
        {
            case 5: return 10_000m;
            case 4: return 100m;
            case 3: return 5m;
            default: return 0m;
        }
    }

    // Реализация абстрактного метода
    public override void CheckResult(List<int> winningNumbers)
    {
        Matches = 0;

        foreach (int number in ChosenNumbers)
        {
            if (winningNumbers.Contains(number))
            {
                Matches++;
            }
        }

        if (Matches == NumbersCount)
        {
            // Сумму джекпота назначит аппарат
            Status = TicketStatus.Won;
            PrizeAmount = 0;
        }
        else
        {
            SetResult(GetPrizeForMatches(Matches));
        }
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"""
        [Числовой] {TicketId} | Владелец : {OwnerId} | 
        Числа : {string.Join("-", ChosenNumbers)} |
        Совпало : {Matches} | {GetStatusText(Status)} | Приз : {PrizeAmount:N2} руб
        """);
    }

    public override void ShowRules()
    {
        Console.WriteLine($"[Числовой билет, {DefaultPrice} руб.] Выберите {NumbersCount} разных чисел от 1 до {MaxNumber}");
        Console.WriteLine("  Призы : 3 числа - 5 руб., 4 - 100 руб., 5 - 10 000 руб., 6 - ДЖЕКПОТ");
    }
}
