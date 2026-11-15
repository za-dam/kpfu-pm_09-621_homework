using System.Globalization;
using System.Numerics;
using static TumakovTasks.UtilitesForSongs;

namespace TumakovTasks;

class Program
{
    static void Main(string[] args)
    {
        // Задания из Лабораторной Тумакова 9 главы

        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\nЗадания из методички Тумакова - Лабораторная 9 главы");
            Console.WriteLine("Task 1 - Упражнение 9.1.-9.3. (Банковский счёт и работа с ним)");
            Console.WriteLine("Task 2 - Домашнее задание 9.4. (Класс песня и демонстрация его работы)");

            Console.Write("\nВыберите номер задания (1-2, 0 для выхода) : ");
            
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 2)
            {
                Console.WriteLine("Ошибка : ожидалось целое число от 0 до 2.");
                continue;
            }

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Завершение работы...");
                    isRunning = false;
                    break;
                case 1: Exercise91(); break;
                case 2: Homework94(); break;
            }
        }
    }

    // Task 1 - Упражнение 9.1.-9.3. (Банковский счёт и работа с ним)
    internal static void Exercise91()
    {
        Console.WriteLine("Task 1 - Упражнение 9.1.-9.3. (Банковский счёт и работа с ним)");
        Console.WriteLine("От пользователя требуется ввести баланс счёта и его тип, а после получить информацию и попытаться положить и снять с счёта, а после перевести на чужой счёт");

        Console.Write("Введите тип счёта (Current/Savings) : ");
        AccountType accountType = Console.ReadLine()?.Trim().ToLower() switch
        {
            "current" => AccountType.Current,
            "savings" => AccountType.Savings,
            _ => throw new ArgumentException("Ошибка : Неверный тип счёта. Допустимы только Current или Savings.")
        };

        decimal balance = ReadPositiveNumber<decimal>("Введите баланс счёта (ожидается положительное число) : ");

        using BankAccount userAccount = new BankAccount(balance, accountType);

        userAccount.PrintInfo();

        Console.WriteLine("Попробуйте пополнить счёт");
        decimal deposit = ReadPositiveNumber<decimal>("Введите сумму пополнения : ");
        if (!userAccount.TryDeposit(deposit))
        {
            Console.WriteLine("Сумма пополнения не может быть отрицательной");
        }
        Console.WriteLine($"Текущий баланс : {userAccount.Balance:F2}");

        Console.WriteLine("Попробуйте снять со счёта");
        decimal withdraw = ReadPositiveNumber<decimal>("Введите сумму снятия : ");
        if (!userAccount.TryWithdraw(withdraw))
        {
            Console.WriteLine("На счёте недостаточно средств");
        }
        Console.WriteLine($"Текущий баланс : {userAccount.Balance:F2}");

        decimal demoAccountBalance = 1000m;
        AccountType demoAccountType = AccountType.Current;
        decimal demoTransferAmount = 500m;

        using BankAccount demoAccount = new BankAccount(demoAccountBalance, demoAccountType);

        Console.WriteLine($"Демонстрационный счёт, который будет переводить сумму - {demoTransferAmount}, на счёт пользователя");
        demoAccount.PrintInfo();

        userAccount.TryMoneyTransfer(demoAccount, demoTransferAmount);

        demoAccount.PrintInfo();
        userAccount.PrintInfo();

        Console.WriteLine("\nИстория операций по обоим счетам будет сохранена в JSON-файлы при завершении");
    }

    // Task 2 - Домашнее задание 9.4. (Класс песня и демонстрация его работы)
    internal static void Homework94()
    {
        Console.WriteLine("Task 2 - Домашнее задание 9.4. (Список песен, связка с предыдущей после добавления новой, сравнение песен из списка)");
        Console.WriteLine("Пользователю предлагается просмотреть работу класса песни и при желание заполнить свой плейлист и сравнить любые 2 песни");

        Song mySong = new Song();
        Console.WriteLine("\nТест пустого конструктора (new Song()) :");
        mySong.Print();

        List<Song> playlist = new List<Song>();

        playlist.Add(new Song("Кукушка", "Кино"));

        playlist.Add(new Song("Группа крови", "Кино", playlist[^1]));
        playlist.Add(new Song("Лесник", "Король и Шут", playlist[^1]));
        playlist.Add(new Song("Кукушка", "Кино", playlist[^1]));

        PrintPlaylist(playlist);
        CheckBinding(playlist);

        bool isResponseValid = false;

        while (!isResponseValid)
        {
            Console.Write("\nХотите заполнить свой плейлист и сравнить любые 2 песни? (yes/no) : ");
            string answer = Console.ReadLine()?.Trim().ToLower()!;

            switch (answer)
            {
                case "yes":
                    isResponseValid = true;
                    playlist.Clear(); 
                    FillUserPlaylist(playlist);
                    PrintPlaylist(playlist);
                    CheckBinding(playlist);
                    CompareTwoSongs(playlist);
                    break;

                case "no":
                    isResponseValid = true;
                    Console.WriteLine("Выход из задания...");
                    break;

                default:
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите 'yes' или 'no'.");
                    break;
            }
        }
    }


    public static T ReadPositiveNumber<T>(string message) where T : INumber<T>
    {
        bool isRunning = true;
        T result = T.Zero;

        while (isRunning)
        {
            Console.Write(message);

            string userValue = Console.ReadLine()?.Replace(',', '.') ?? string.Empty;

            if (T.TryParse(userValue, CultureInfo.InvariantCulture, out T value) && value > T.Zero)
            {
                isRunning = false;
                result = value;
            }
            else
            {
                Console.WriteLine("Ошибка : Ожидается положительное число.");
            }
        }

        return result;
    }
}
