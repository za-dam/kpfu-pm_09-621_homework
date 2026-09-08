using System;
using System.Linq;
using System.Threading;

namespace tumakov;
//Задания после второй главы из методички Тумакова Д.Н. 
class Program
{
    static void Main(string[] args)
    {
        //Упражнение 2.1
        Console.WriteLine("Упражнение 2.1");
        Console.Write("Введите ваше имя: ");
        string? name = Console.ReadLine();
        Console.WriteLine($"Привет, {name}!");
        Console.ReadKey();

        //Упражнение 2.2
        Console.WriteLine("Упражнение 2.2");
        try
        {
            Console.Write("Введите первое целое число: ");
            int? num1 = int.Parse(Console.ReadLine()!);
            Console.Write("Введите второе целое число: ");
            int? num2 = int.Parse(Console.ReadLine()!);
            int? result = num1 / num2; 
            Console.WriteLine($"Результат деления: {result}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Ошибка: Деление на ноль невозможно!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введено не корректное целое число!");
        }
        Console.ReadKey();

        //Домашнее задание 2.1
        Console.WriteLine("Домашнее задание 2.1");
         Console.Write("Введите одну букву: ");
        string? input = Console.ReadLine();
        char inputChar = input[0];
        char nextChar = (char)(inputChar + 1);
        Console.WriteLine("Следующая буква: " + nextChar);
        Console.ReadKey();

        //Домашнее задание 2.2
        Console.WriteLine("Домашнее задание 2.2");
        Console.WriteLine("Задание 10");
        Console.WriteLine("Решение квадратного уравнения вида : ax^2 + bx + c = 0");
        Console.ReadKey();
        Console.WriteLine("Введите коэффициент a перед x^2 (если число не целое, то введите его дробную часть через запятую) : ");
        string? coef_a = Console.ReadLine();
        double db_coef_a;
        Console.WriteLine("Введите коэффициент b перед x (если число не целое, то введите его дробную часть через запятую) : ");
        string? coef_b = Console.ReadLine();
        double db_coef_b;
        Console.WriteLine("Введите свободный коэффициент c (если число не целое, то введите его дробную часть через запятую) : ");
        string? coef_c = Console.ReadLine();
        double db_coef_c;
        if (double.TryParse(coef_a, out db_coef_a) && double.TryParse(coef_b, out db_coef_b) && double.TryParse(coef_c, out db_coef_c))
        {
            double dscr = Math.Pow(db_coef_b, 2) - 4 * db_coef_a * db_coef_c;
            if (dscr > 0)
            {
                double x1 = (-db_coef_b + Math.Sqrt(dscr)) / (2 * db_coef_a);
                double x2 = (-db_coef_b - Math.Sqrt(dscr)) / (2 * db_coef_a);
                Console.WriteLine($"Корни квадратного уравнения : x1 = {x1}, x2 = {x2}");
            }
            else if (dscr == 0) Console.WriteLine($"Единственный корень квадратного уравнения : x = {(-db_coef_b) / (2 * db_coef_a)}");
            else Console.WriteLine("Вещественных корней нет, дискриминант меньше нуля");
        }
        else Console.WriteLine("Некорректный ввод");
        Console.ReadKey();
    }
}