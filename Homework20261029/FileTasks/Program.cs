using static FileTasks.Utilites;

namespace FileTasks;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Программа лотереи : демонстрация ООП ---");
 
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n1 - О лотерее и принципах ООП в этой программе");
            Console.WriteLine("2 - Автоматическая демонстрация (готовые данные)");
            Console.WriteLine("3 - Сыграть самому (ввод с клавиатуры)");
            Console.WriteLine("0 - Выход");
 
            int choice = ReadIntInRange("Выбор : ", 0, 3);
 
            switch (choice)
            {
                case 0:
                    Console.WriteLine("Завершение работы...");
                    isRunning = false;
                    break;
                case 1: ShowTheory(); break;
                case 2: RunDemo(); break;
                case 3: PlayInteractive(); break;
            }
        }
    }


    internal static void ShowTheory()
    {
        Console.WriteLine("\n--- Что такое лотерея и что есть в текущей реализации ---");
        Console.WriteLine("""
        Лотерея - игра на удачу. Игроки покупают билеты, лотерейный автомат проводит тираж, приз определяется случайно. 
        Часть денег от продаж уходит в джекпот - главный приз, который растёт, пока его никто не выиграл.");
        В программе три вида билетов :
            Числовой - выбираете 6 чисел из 49, выигрыш зависит от числа совпадений;
            VIP      - числовой + бонусное число, которое удваивает приз;
            Скретч   - стираете слой и сразу видите результат;
        """);
 
        Console.WriteLine("\n--- Принципы ООП в программе ---");
 
        Console.WriteLine("""
        1. Абстракция - класс LotteryTicket :
            Описывает только общее у любого билета : владелец, цена, статус, приз;
            Создать просто 'билет' нельзя, только конкретный : числовой или скретч;
        """);
 
        Console.WriteLine("""
        2. Инкапсуляция - данные спрятаны, доступ через свойства :
            Приз скретча (_hiddenPrize) закрыт, пока слой не стёрт;
            Status и PrizeAmount меняет только сам билет;
            Баланс игрока меняется только методами TopUp / TryPay / AddWinnings;
            OwnerName и Age можно записать, но неверное значение вызовет ошибку;
        """);
 
        Console.WriteLine("""
        3. Наследование :
            LotteryTicket -> NumberTicket -> VipNumberTicket;
            LotteryTicket -> ScratchTicket;
        """);
 
        Console.WriteLine("""
        4. Полиморфизм :
            Аппарат вызывает ticket.CheckResult() у списка LotteryTicket, не зная какой это билет;
            Числовой считает совпадения, скретч стирает слой, VIP дополнительно смотрит бонус;
        """);
    }

    static void RunDemo()
    {
        Console.WriteLine("\n--- Демонстрация ---");
 
        LotteryMachine machine = new LotteryMachine("Лото-Экспресс");

        PrintHeader("Шаг 1. Создание игроков");
 
        Player alexey = new Player("Алексей", new DateTime(2000, 10, 28), 2000m);
        Player elena = new Player("Елена", new DateTime(1997, 1, 14), 500m);
        Player ivan = new Player("Иван", new DateTime(2012, 6, 12), 300m);
        Player maria = new Player("Мария", new DateTime(1980, 10, 16));   // второй конструктор : баланс 0
        Player petr = new Player("Петр", new DateTime(1980, 12, 31));
        maria.TopUp(1000m);
 
        machine.RegisterPlayer(alexey);
        machine.RegisterPlayer(elena);
        machine.RegisterPlayer(ivan);
        machine.RegisterPlayer(maria);
        machine.PrintPlayers();
 
        Console.WriteLine("\nПробуем записать в свойство Birthdate неверное значение так, чтобы возраст стал отрицательным : ");
        try
        {
            ivan.BirthDate = new DateTime(2034, 8, 12);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Перехвачена ошибка : {ex.Message}");
        }
 

        PrintHeader("Шаг 2. Покупка билетов");
 
        List<LotteryTicket> sold = new List<LotteryTicket>();
 
        NumberTicket alexeyTicket = new NumberTicket(alexey.Id, new List<int> { 5, 12, 23, 42, 10, 7 });
        VipNumberTicket vipTicket = new VipNumberTicket(alexey.Id, new List<int> { 5, 12, 23, 40, 41, 2 }, 44);
        NumberTicket quickTicket = new NumberTicket(maria.Id);           // быстрый выбор
        ScratchTicket luckyScratch = new ScratchTicket(elena.Id, 500m);  // приз под слоем задан
        ScratchTicket randomScratch = new ScratchTicket(elena.Id);       // приз случайный
        ScratchTicket ivanScratch = new ScratchTicket(ivan.Id);
 
        Sell(machine, sold, alexeyTicket);
        Sell(machine, sold, vipTicket);
        Sell(machine, sold, quickTicket);
        Sell(machine, sold, luckyScratch);
        Sell(machine, sold, randomScratch);
        Sell(machine, sold, ivanScratch);   // Иван ещё несовершеннолетний, ему откажут
 
        Console.WriteLine("\nПробуем создать билет с неверными числами (всего 3 числа) : ");
        try
        {
            _ = new NumberTicket(alexey.Id, new List<int> { 1, 2, 3 });
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Перехвачена ошибка : {ex.Message}");
        }
 

        PrintHeader("Шаг 3. Передача билета");
 
        machine.TransferTicket(quickTicket.TicketId, elena.Id);   // подарок Марии Елене
        machine.TransferTicket(quickTicket.TicketId, petr.Id);    // Петра нет в системе
 

        PrintHeader("Шаг 4. Полиморфизм, демострация через один вызов, но разное поведение");
 
        Console.WriteLine("Правила (ShowRules) трёх разных билетов : ");
        List<LotteryTicket> samples = new List<LotteryTicket> { alexeyTicket, vipTicket, luckyScratch };
        foreach (LotteryTicket ticket in samples)
        {
            ticket.ShowRules();
        }
 
        Console.WriteLine("\nСостояние всех проданных билетов до тиража (DisplayInfo) : ");
        foreach (LotteryTicket ticket in sold)
        {
            ticket.DisplayInfo();
        }
 

        PrintHeader("Шаг 5. Тираж с заданными числами (чтобы результат был предсказуем)");
        Console.WriteLine("Алексей угадал 3 числа, VIP-билет угадал 5 и ещё выпал бонус 44 (приз x2)");
 
        machine.RunDraw(new List<int> { 5, 12, 23, 40, 41, 44 });
 
        
        PrintHeader("Шаг 6. Джекпот, Алексей покупает билет и угадывает все 6 чисел");
 
        NumberTicket jackpotTicket = new NumberTicket(alexey.Id, new List<int> { 1, 2, 3, 4, 5, 6 });
        Sell(machine, sold, jackpotTicket);
        machine.RunDraw(new List<int> { 1, 2, 3, 4, 5, 6 });
 

        PrintHeader("Шаг 7. RunDraw() без параметров, числа выпадают случайно");
 
        Sell(machine, sold, new NumberTicket(maria.Id));
        machine.RunDraw();
 

        PrintHeader("Итоги");
        machine.PrintRegistry();
        machine.PrintPlayers();
        machine.PrintStatistics();
    }
 
    private static void Sell(LotteryMachine machine, List<LotteryTicket> sold, LotteryTicket ticket)
    {
        if (machine.SellTicket(ticket))
        {
            sold.Add(ticket);
        }
    }
 

    // Игра с вводом с клавиатуры 
    internal static void PlayInteractive()
    {
        LotteryMachine machine = new LotteryMachine();
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n--- ИГРА ---");
            Console.WriteLine("1 - Зарегистрировать игрока");
            Console.WriteLine("2 - Купить числовой билет (100 руб.)");
            Console.WriteLine("3 - Купить VIP-билет (300 руб.)");
            Console.WriteLine("4 - Купить скретч-билет (50 руб.)");
            Console.WriteLine("5 - Провести тираж");
            Console.WriteLine("6 - Реестр билетов");
            Console.WriteLine("7 - Игроки и статистика");
            Console.WriteLine("8 - Пополнить баланс игрока");
            Console.WriteLine("9 - Передать билет другому игроку");
            Console.WriteLine("0 - Назад");
 
            int choice = ReadIntInRange("Выбор : ", 0, 9);
 
            switch (choice)
            {
                case 0: 
                    isRunning = false;
                    break;
                case 1: RegisterPlayerInteractive(machine); break;
                case 2: BuyNumberTicketInteractive(machine, false); break;
                case 3: BuyNumberTicketInteractive(machine, true); break;
                case 4: BuyScratchInteractive(machine); break;
                case 5: machine.RunDraw(); break;
                case 6: machine.PrintRegistry(); break;
                case 7:
                    machine.PrintPlayers();
                    machine.PrintStatistics();
                    break;
                case 8: TopUpInteractive(machine); break;
                case 9: TransferInteractive(machine); break;
            }
        }
    }
 
    private static void RegisterPlayerInteractive(LotteryMachine machine)
    {
        string name = ReadText("Имя игрока: ");
        DateTime birthDate = ReadDateTimeExact("Дата рожения игрока : ");
        decimal balance = ReadDecimal("Начальный баланс (руб.) : ");
 
        try
        {
            machine.RegisterPlayer(new Player(name, birthDate, balance));
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка : {ex.Message}");
        }
    }
 
    static Player? AskPlayer(LotteryMachine machine)
    {
        bool isPlayerId = Guid.TryParse(ReadText("Id игрока : "), out Guid playerId);
        Player? player = machine.FindPlayer(playerId);
 
        if (player == null || isPlayerId)
        {
            Console.WriteLine("Игрок не найден. Сначала зарегистрируйте его (пункт 1)");
        }
 
        return player;
    }
 
    static void BuyNumberTicketInteractive(LotteryMachine machine, bool isVip)
    {
        Player? player = AskPlayer(machine);
        if (player == null)
        {
            return;
        }
 
        Console.WriteLine("1 - выбрать числа самому, 2 - быстрый выбор (числа выберет машина)");
        int mode = ReadIntInRange("Режим: ", 1, 2);
 
        LotteryTicket ticket;
 
        if (mode == 2)
        {
            if (isVip)
            {
                ticket = new VipNumberTicket(player.Id);
            }
            else
            {
                ticket = new NumberTicket(player.Id);
            }
        }
        else
        {
            List<int>? numbers = ReadNumbers($"Введите {NumberTicket.NumbersCount} разных чисел от 1 до {NumberTicket.MaxNumber} через пробел : ");
            if (numbers == null)
            {
                return;
            }
 
            if (!NumberTicket.IsValidSet(numbers, out string error))
            {
                Console.WriteLine($"Ошибка: {error}");
                return;
            }
 
            if (isVip)
            {
                int bonus = ReadIntInRange("Бонусное число (не из выбранных) : ", 1, NumberTicket.MaxNumber);
 
                if (!VipNumberTicket.IsValidBonus(bonus, numbers))
                {
                    Console.WriteLine("Ошибка : бонусное число совпадает с выбранным.");
                    return;
                }
 
                ticket = new VipNumberTicket(player.Id, numbers, bonus);
            }
            else
            {
                ticket = new NumberTicket(player.Id, numbers);
            }
        }
 
        machine.SellTicket(ticket);
    }
 
    static void BuyScratchInteractive(LotteryMachine machine)
    {
        Player? player = AskPlayer(machine);
        if (player == null)
        {
            return;
        }
 
        machine.SellTicket(new ScratchTicket(player.Id));
    }
 
    static void TopUpInteractive(LotteryMachine machine)
    {
        Player? player = AskPlayer(machine);
        if (player == null)
        {
            return;
        }
 
        decimal amount = ReadDecimal("Сумма пополнения (руб.) : ");
 
        if (player.TopUp(amount))
        {
            Console.WriteLine($"Баланс {player.Name} : {player.Balance:N2} руб.");
        }
        else
        {
            Console.WriteLine("Ошибка : сумма должна быть больше нуля.");
        }
    }
 
    static void TransferInteractive(LotteryMachine machine)
    {
        string ticketId = ReadText("Номер билета (например, T-0001) : ");
        bool isNewOwnerId = Guid.TryParse(ReadText("Id нового владельца : "), out Guid newOwnerId);
        
        if (isNewOwnerId)
            machine.TransferTicket(ticketId, newOwnerId);
    }
}
