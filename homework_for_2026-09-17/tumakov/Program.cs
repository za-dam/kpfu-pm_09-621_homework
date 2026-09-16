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

        AccountType account1 = AccountType.Current;
        Console.WriteLine($"Тип счёта: {account1}");
        Console.ReadKey();

        //Упражнение 3.2
        Console.WriteLine("\nУпражнение 3.2 - Создать struct BankAccount с тремя полями: \n" +
        "номер счёта, тип (из задания 3.1), баланс. Заполнить значениями и напечатать.");

        BankAccount account2 = new BankAccount(
            "40817810099910004312",
            AccountType.Savings,
            125000.50m);
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

        Employee worker = new Employee("Иванов Иван Иванович", University.KAI);
        Console.WriteLine($"""
        Информация о работнике :
        Имя:  {worker.Name}
        ВУЗ:  {worker.University}
        """);
        Console.ReadKey();
    }
}