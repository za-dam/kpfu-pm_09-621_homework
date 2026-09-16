using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using MyLib.File;

namespace file;

class Program
{
    static void Main(string[] args)
    {
        //Задание 1
        Console.WriteLine("Задание 1 - Для каждого базового типа C# вывести: имя типа — максимум — минимум.");
        
        Console.WriteLine($"""
            Тип данных – Максимум – Минимум
            sbyte      – {sbyte.MaxValue} – {sbyte.MinValue}
            byte       – {byte.MaxValue} – {byte.MinValue}
            short      – {short.MaxValue} – {short.MinValue}
            ushort     – {ushort.MaxValue} – {ushort.MinValue}
            int        – {int.MaxValue} – {int.MinValue}
            uint       – {uint.MaxValue} – {uint.MinValue}
            long       – {long.MaxValue} – {long.MinValue}
            ulong      – {ulong.MaxValue} – {ulong.MinValue}
            float      – {float.MaxValue} – {float.MinValue}
            double     – {double.MaxValue} – {double.MinValue}
            decimal    – {decimal.MaxValue} – {decimal.MinValue}
            char       – {(int)char.MaxValue} – {(int)char.MinValue}
            """);
        Console.ReadKey();

        //Задание 2
        Console.WriteLine("\nЗадание 2 -  Считать 4 значения, сохранить в переменные, напечатать.");

        Console.Write("Введите имя: ");
        string name2 = Console.ReadLine()!;
        Console.Write("Введите город: ");
        string city2 = Console.ReadLine()!;
        Console.Write("Введите возраст: ");
        int age2 = int.Parse(Console.ReadLine()!);
        Console.Write("Введите PIN-код (4 цифры): ");
        string pin2 = Console.ReadLine()!;

        User user2 = new User(name2, city2, age2, pin2);
        Console.WriteLine("\n"+ $"""
        Данные пользователя :
        Имя :     {user2.Name}
        Город :   {user2.City}
        Возраст : {user2.Age}
        PIN-код : {user2.Pin}
        """);
        Console.ReadKey();

        //Задание 3
        Console.WriteLine("\nЗадание 3 - Ввести строку, заменить строчные буквы на заглавные и наоборот. \n" +
        "Остальные символы не трогать.");

        Console.Write("Введите строку: ");
        string input = Console.ReadLine()!;
        string result = "";
        foreach (char c in input)
        {
            if (char.IsLower(c))
                result += char.ToUpper(c);
            else if (char.IsUpper(c))
                result += char.ToLower(c);
            else
                result += c;
        }
        Console.WriteLine($"Было:  {input}");
        Console.WriteLine($"Стало: {result}");
        Console.ReadKey();

        //Задание 4
        Console.WriteLine("\nЗадание 4 - Ввести строку и подстроку. Посчитать, сколько раз подстрока встречается в строке.");

        Console.Write("Введите строку: ");
        string text4 = Console.ReadLine()!;
        Console.Write("Введите подстроку: ");
        string sub4 = Console.ReadLine()!;
        Console.WriteLine($"Кол-во вхождений : {CountSubarray(text4, sub4)}");
        Console.ReadKey();

        //Задание 5
        Console.WriteLine("\nЗадание 5 - Даны: normPrice (обычная цена), salePrice (скидка в %), holidayPrice (стоимость отпуска). \n" + 
        "Вернуть, сколько бутылок надо купить, чтобы сэкономленные деньги покрыли отпуск. Округлить вниз.");

        Console.Write("Обычная цена бутылки: ");
        int normPrice5 = int.Parse(Console.ReadLine()!);
        Console.Write("Скидка в Duty Free (%): ");
        int salePrice5 = int.Parse(Console.ReadLine()!);
        Console.Write("Стоимость отпуска: ");
        int holidayPrice5 = int.Parse(Console.ReadLine()!);
        int bottles5 = DutyFree(normPrice5, salePrice5, holidayPrice5);
        Console.WriteLine($"Нужно купить бутылок: {bottles5}");
        Console.ReadKey();

        //Задание 6
        Console.WriteLine("\nЗадание 6 -  Описать Drink (название, % спирта) и \n" +
        "Student (ФИО, ID, дата рождения, категория алкоголизма, объём выпитого, напиток). \n" +
        "Создать 5 студентов, посчитать: \n" +
        "общий объём выпитого, общий объём чистого спирта, для каждого — % жидкости и % спирта от общего.");

        Drink beer = new Drink("Пиво", 4.5);
        Drink wine = new Drink("Вино", 12.0);
        Drink vodka = new Drink("Водка", 40.0);
        Drink whiskey = new Drink("Виски", 43.0);
        Drink lemonade = new Drink("Лимонад",  0.0);

        Student[] students = new Student[]
        {
            new Student("Иванов",  "Иван",   101, new DateTime(2003, 5, 12),  AlcoCategory.Alcoholic,   1500, vodka),
            new Student("Петров",  "Пётр",   102, new DateTime(2004, 2, 28),  AlcoCategory.Drinker,     2000, beer),
            new Student("Сидоров", "Семён",  103, new DateTime(2005, 8, 3),   AlcoCategory.HolidayOnly,  750, wine),
            new Student("Кузнецов","Кирилл", 104, new DateTime(2003, 11, 19), AlcoCategory.Abstainer,      0, lemonade),
            new Student("Смирнов", "Сергей", 105, new DateTime(2004, 7, 7),   AlcoCategory.Drinker,      500, whiskey)
        };

        double totalVolume  = 0;
        double totalAlcohol = 0;

        foreach (Student s in students)
        {
            totalVolume  += s.VolumeMl;
            totalAlcohol += s.PureAlcoholMl();
        }

        string rows = "";
        foreach (Student s in students)
        {
            double alcohol = s.PureAlcoholMl();
            double pVol;
            double pAlc;
            if (totalVolume > 0)
            {
                pVol = (s.VolumeMl / totalVolume) * 100 ;
            }
            else 
            {
                pVol = 0;
            }
            if (totalAlcohol > 0)
            {
                pAlc = (alcohol / totalAlcohol) * 100; 
            }
            else
            {
                pAlc = 0;
            } 

            rows += $"{s.LastName} {s.FirstName} — {s.Category} — {s.Drink.Name} " +
                    $"{s.VolumeMl:F1} мл, спирта {alcohol:F1} мл" +
                    $"({pVol:F1}% жидк., {pAlc:F1}% спирта)\n";
        }

        Console.WriteLine($"""
            Студенты и алкоголь:
            {rows}
            ИТОГО: {totalVolume} мл жидкости, {totalAlcohol} мл спирта
            """);

        Console.ReadKey();
    }
    public static int CountSubarray(string arr, string subArr)
    {
        if (subArr.Length == 0 || subArr.Length > arr.Length)
            return 0;
        int count = 0;
        for (int i = 0; i <= arr.Length - subArr.Length; i++)
        {
            string piece = arr[i..(i + subArr.Length)];
            if (piece == subArr)
                count++;
        }
        return count;
    }
    public static int DutyFree(int normPrice, int salePrice, int holidayPrice)
    {
        if (normPrice <= 0 || salePrice <= 0)
            return 0;
        return holidayPrice * 100 / (normPrice * salePrice);
    }
}   