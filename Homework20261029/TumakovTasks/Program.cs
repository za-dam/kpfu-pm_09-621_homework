using System.Globalization;
using System.Numerics; 


namespace TumakovTasks;

class Program
{
    static void Main(string[] args)
    {
        //Задания из методички Тумакова - Лабораторная 7 главы
        Console.WriteLine("Задания из методички Тумакова - Лабораторная 7 главы");
        Console.WriteLine("Task 1 - Упражнения 7.1 - 7.3 (Банковский счёт и работа с ним)");
        Console.WriteLine("Task 2 - Домашнее задание 7.1 (Здание с расчетными методами)");

        bool isRunning = true;

        while (isRunning)
        {
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
                case 1: Exercise71_73(); break;
                case 2: Homework71(); break;
            }
        }
    }

    // Упражнения 7.1 - 7.3 (Банковский счёт и работа с ним)
    internal static void Exercise71_73()
    {
        Console.WriteLine("Task 1 - Упражнения 7.1 - 7.3 (Банковский счёт и работа с ним)");
        Console.WriteLine("От пользователя требуется ввести баланс счёта и его тип, а после получить информацию и попытаться положить и снять с счёта");

        Console.Write("Введите тип счёта (Current/Savings) : ");
        AccountType accountType = Console.ReadLine()?.Trim().ToLower() switch
        {
            "current" => AccountType.Current,
            "savings" => AccountType.Savings,
            _ => throw new ArgumentException("Ошибка : Неверный тип счёта. Допустимы только Current или Savings.")
        };

        decimal balance = ReadPositiveNumber<decimal>("Введите баланс счёта (ожидается положительное число) : ");

        BankAccount userAccount = new BankAccount(balance, accountType);

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
    }

    // Домашнее задание 7.1 (Здание с расчетными методами)
    internal static void Homework71()
    {
        Console.WriteLine("Task 2 - Домашнее задание 7.1 (Здание с расчетными методами)");
        Console.WriteLine("От пользователя требуется задать высоту здания, этажность, количество квартир и подъездов");

        double height = ReadPositiveNumber<double>("Введите высоту здания : ");
        ushort floors = ReadPositiveNumber<ushort>("Введите кол-во этажей : ");
        ushort apartments = ReadPositiveNumber<ushort>("Введите кол-во квартир : ");
        ushort entrances = ReadPositiveNumber<ushort>("Введите кол-во подъездов : ");

        Building firstBuild = new Building(height, floors, apartments, entrances);

        Console.WriteLine(firstBuild.GetInfo());
        firstBuild.PrintEvenDistributionOfFloors();
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
