using MyLibrary.Tumakov;


namespace HomeworkFromTumakov;

class Program
{
    static void Main(string[] args)
    {
        //Задания из методички Тумакова - Лабораторная 4 главы
        Console.WriteLine("Задания из методички Тумакова - Лабораторная 4 главы");
        Console.ReadKey();

        //Упражнение 4.1 (4.2) (Домашнее задание 4.1) - перевод числа в месяц и день числа (проверка введенного числа)
        Console.WriteLine("Упражнение 4.1 (4.2) (Домашнее задание 4.1) - перевод числа в месяц и день числа (проверка введенного числа)");
        try
        {
            Console.Write("Введите год (больше нуля): ");
            int year = int.Parse(Console.ReadLine()!);
            if (year < 1)
                throw new ArgumentOutOfRangeException(nameof(year), year, "Число вне диапазона");
            bool isLeap = DateHelper.IsLeapYear(year);
            int maxDay = isLeap ? 366 : 365;
            Console.Write($"Введите номер дня (1 - {maxDay}): ");
            int dayOfYear = int.Parse(Console.ReadLine()!);
            if (dayOfYear < 1 || dayOfYear > maxDay)
                throw new ArgumentOutOfRangeException(nameof(dayOfYear), dayOfYear, "Число вне диапазона");
            int[] daysInMonth = DateHelper.GetDaysInMonth(isLeap);

            Console.WriteLine($"Результат : {year} год, {DateHelper.ToDateString(dayOfYear, daysInMonth)}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: введено не целое число");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Ошибка: число должно быть в диапазоне");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Ошибка: слишком большое число.");
        }
        Console.ReadKey();
    }
}