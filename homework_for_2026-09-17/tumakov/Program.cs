using System;
using System.Linq;
using System.Threading;
using MyLib.Tumakov;

namespace tumakov;

class Program
{
    static void Main(string[] args)
    {
        //Упражнение 3.1
        Console.WriteLine("Упражнение 3.1 - Описать перечисление AccountType со значениями : \n" +
        "Current (текущий) и Savings (сберегательный). \n" +
        "Создать переменную этого типа, присвоить значение, вывести.");

        Console.WriteLine("Введите тип счёта (текущий / сберегательный) : ");
        AccountType accountType1 = Console.ReadLine()! switch
        {
            "текущий" => AccountType.Current,
            "сберегательный" => AccountType.Savings,
            _ => AccountType.Unknown
        };
        Console.WriteLine($"Тип счёта: {accountType1}");
        Console.ReadKey();

        //Упражнение 3.2
        Console.WriteLine("\nУпражнение 3.2 - Создать struct BankAccount с тремя полями: \n" +
        "номер счёта, тип (из задания 3.1), баланс. Заполнить значениями и напечатать.");

        Console.WriteLine("Введите номер счёта : ");
        string accountNumber2 = Console.ReadLine()!;
        Console.WriteLine("Введите тип счёта (текущий / сберегательный) : ");
        AccountType accountType2 = Console.ReadLine()!.Trim().ToLower() switch
        {
            "текущий" => AccountType.Current,
            "сберегательный" => AccountType.Savings,
            _ => AccountType.Unknown
        };
        Console.WriteLine("Введите баланс счёта : ");
        decimal accountBalance2 = decimal.Parse(Console.ReadLine()!);

        BankAccount account2 = new BankAccount(
            accountNumber2,
            accountType2,
            accountBalance2
            );
        Console.WriteLine($"""
            Информация о счёте :
            Номер счёта: {account2.AccountNumber}
            Тип счёта:   {account2.Type}
            Баланс:      {account2.Balance} руб.
            """);
        Console.ReadKey();

        //Домашнее задание 3.1
        Console.WriteLine("\nДомашнее задание 3.1 - Описать enum University { KGU, KAI, KHTI }. \n" +
        "Создать struct Employee с двумя полями — имя (string) и ВУЗ (University). \n" +
        "Заполнить и распечатать.");

        Console.Write("\nВведите имя работника: ");
        string name3 = Console.ReadLine()!;
        Console.Write("Введите ВУЗ (КГУ / КАИ / КХТИ): ");
        University university3 = Console.ReadLine()!.Trim().ToLower() switch
        {
            "кгу" => University.KGU,
            "каи" => University.KAI,
            "кхти" => University.KHTI,
            _ => University.Unknown
        };

        Employee worker = new Employee();
        Console.WriteLine($"""
        Информация о работнике :
        Имя:  {worker.Name}
        ВУЗ:  {worker.University}
        """);
        Console.ReadKey();
    }
}