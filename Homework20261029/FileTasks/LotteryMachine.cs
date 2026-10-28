namespace FileTasks;

public class LotteryMachine
{
    private const decimal StartJackpot = 1_000_000m;
    private const decimal JackpotGrowth = 0.1m;
 
    private readonly List<LotteryTicket> _tickets = new List<LotteryTicket>();
 
    private readonly Dictionary<Guid, Player> _players =
        new Dictionary<Guid, Player>();
 
    private string _name = string.Empty;
 
    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Название лотереи не может быть пустым.");
            }
            _name = value.Trim();
        }
    }
 
    public decimal Jackpot { get; private set; } = StartJackpot;
    public decimal Revenue { get; private set; }
    public decimal TotalPayouts { get; private set; }
    public int DrawsCount { get; private set; }
 
    public int TicketsCount
    {
        get { return _tickets.Count; }
    }
 
    public decimal Profit
    {
        get { return Revenue - TotalPayouts; }
    }
 
    public LotteryMachine(string name)
    {
        Name = name;
    }
 
    public LotteryMachine() : this("Лотерея")
    {
    }
 

    public bool RegisterPlayer(Player player)
    {
        if (_players.ContainsKey(player.Id))
        {
            Console.WriteLine($"Игрок с Id : '{player.Id}' уже зарегистрирован");
            return false;
        }
 
        _players.Add(player.Id , player);
        Console.WriteLine($"Зарегистрирован игрок : {player.Name}");
        return true;
    }
 
    public Player? FindPlayer(Guid id)
    {
        if (_players.TryGetValue(id, out Player? player))
        {
            return player;
        }
 
        return null;
    }
 
    public bool SellTicket(LotteryTicket ticket)
    {
        Player? buyer = FindPlayer(ticket.OwnerId);
 
        if (buyer == null)
        {
            Console.WriteLine($"Отказ : игрок «{ticket.OwnerId.ToString("N").Substring(0, 8).ToUpper()}» не зарегистрирован");
            return false;
        }
 
        if (!buyer.IsAdult)
        {
            Console.WriteLine($"Отказ : {buyer.Id} младше 18 лет, билеты ему продавать нельзя");
            return false;
        }
 
        if (!buyer.TryPay(ticket.Price))
        {
            Console.WriteLine($"Отказ : у {buyer.Id} не хватает денег ({buyer.Balance:N2} руб., нужно {ticket.Price:N2} руб.)");
            return false;
        }
 
        _tickets.Add(ticket);
        Revenue += ticket.Price;
        Console.WriteLine($"Продан билет {ticket.GetShortInfo()} за {ticket.Price:N2} руб.");
        return true;
    }
 
    private LotteryTicket? FindTicket(string ticketId)
    {
        foreach (LotteryTicket ticket in _tickets)
        {
            if (string.Equals(ticket.TicketId, ticketId.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return ticket;
            }
        }
 
        return null;
    }
 
    public bool TransferTicket(string ticketId, Guid newOwnerId)
    {
        LotteryTicket? ticket = FindTicket(ticketId);
 
        if (ticket == null)
        {
            Console.WriteLine($"Отказ : билет «{ticketId}» не найден");
            return false;
        }
 
        if (ticket.Status != TicketStatus.Purchased)
        {
            Console.WriteLine("Отказ : билет уже участвовал в тираже, передать его нельзя");
            return false;
        }
 
        Player? receiver = FindPlayer(newOwnerId);
 
        if (receiver == null || !receiver.IsAdult)
        {
            Console.WriteLine($"Отказ : «{newOwnerId}» не зарегистрирован или младше 18 лет");
            return false;
        }
 
        ticket.ChangeOwner(newOwnerId);
        return true;
    }
 

    public void RunDraw()
    {
        RunDraw(NumberTicket.GenerateRandomNumbers());
    }

    public void RunDraw(List<int> winningNumbers)
    {
        if (!NumberTicket.IsValidSet(winningNumbers, out string error))
        {
            Console.WriteLine($"Ошибка : выигрышные числа заданы неверно. {error}");
            return;
        }
 
        List<LotteryTicket> inDraw = new List<LotteryTicket>();
        decimal drawSales = 0m;
 
        foreach (LotteryTicket ticket in _tickets)
        {
            if (ticket.Status == TicketStatus.Purchased)
            {
                inDraw.Add(ticket);
                drawSales += ticket.Price;
            }
        }
 
        if (inDraw.Count == 0)
        {
            Console.WriteLine("Нет билетов, ожидающих тиража");
            return;
        }
 
        DrawsCount++;
        Console.WriteLine($"\n---- Тираж № {DrawsCount} ({Name}) ----");
        Console.WriteLine($"Выпавшие числа : {string.Join(", ", winningNumbers)}");
 
        foreach (LotteryTicket ticket in inDraw)
        {
            ticket.CheckResult(winningNumbers);
        }
 
        ProcessJackpot(inDraw, drawSales);
 
        Console.WriteLine("\nРезультаты билетов : ");
        foreach (LotteryTicket ticket in inDraw)
        {
            ticket.DisplayInfo();
 
            if (ticket.IsWinner())
            {
                PayPrize(ticket);
            }
        }
    }
 
    // Джекпот делится между угадавшими всё, а если таких нет, то джекпот растёт
    private void ProcessJackpot(List<LotteryTicket> inDraw, decimal drawSales)
    {
        List<LotteryTicket> winners = new List<LotteryTicket>();
 
        foreach (LotteryTicket ticket in inDraw)
        {
            if (ticket.IsJackpotWinner)
            {
                winners.Add(ticket);
            }
        }
 
        if (winners.Count > 0)
        {
            decimal share = Math.Round(Jackpot / winners.Count, 2);
 
            foreach (LotteryTicket ticket in winners)
            {
                ticket.AwardJackpot(share);
            }
 
            Console.WriteLine($"\nДЖЕКПОТ {Jackpot:N2} руб. сорван! Победителей : {winners.Count}, каждому по {share:N2} руб.");
            Jackpot = StartJackpot;
        }
        else
        {
            decimal growth = drawSales * JackpotGrowth;
            Jackpot += growth;
            Console.WriteLine($"\nДжекпот никто не сорвал. Он вырос на {growth:N2} руб. и теперь {Jackpot:N2} руб.");
        }
    }

    private void PayPrize(LotteryTicket ticket)
    {
        Player? owner = FindPlayer(ticket.OwnerId);
 
        if (owner == null)
        {
            Console.WriteLine($"---> Владелец «{ticket.OwnerId}» не найден, выплата не произведена");
            return;
        }
 
        owner.AddWinnings(ticket.PrizeAmount);
        TotalPayouts += ticket.PrizeAmount;
        Console.WriteLine($"---> {owner.Name} получает {ticket.PrizeAmount:N2} руб.");
    }

    public void PrintRegistry()
    {
        Console.WriteLine("\n---- РЕЕСТР БИЛЕТОВ ----");
 
        if (_tickets.Count == 0)
        {
            Console.WriteLine("Билетов пока нет");
            return;
        }
 
        foreach (LotteryTicket ticket in _tickets)
        {
            ticket.DisplayInfo();
        }
    }
 
    public void PrintPlayers()
    {
        Console.WriteLine("\n---- ИГРОКИ ----");
 
        if (_players.Count == 0)
        {
            Console.WriteLine("Игроков пока нет");
            return;
        }
 
        foreach (Player player in _players.Values)
        {
            player.ShowInfo();
        }
    }
 
    public void PrintStatistics()
    {
        Console.WriteLine($"""
        \n---- СТАТИСТИКА «{Name}» ----
        Тиражей проведено : {DrawsCount} |
        Билетов продано : {TicketsCount} |
        Выручка : {Revenue:N2} руб. |
        Выплачено призов : {TotalPayouts:N2} руб. |
        Прибыль лотереи : {Profit:N2} руб. |
        Текущий джекпот: {Jackpot:N2} руб.
        """);
    }
}
