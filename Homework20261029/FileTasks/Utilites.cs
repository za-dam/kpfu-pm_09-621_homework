using System.Globalization;
using System.IO.Pipelines;


namespace FileTasks;

public static class Utilites
{
    public static int ReadIntInRange(string message, int min, int max)
    {
        bool isRunning = true;
        int result = 0;

        while (isRunning)
        {
            Console.Write(message);
 
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                result = value;
                isRunning = false;
            }
            else
            {
                Console.WriteLine($"Ошибка : введите целое число от {min} до {max}.");
            }
        }

        return result;
    }

    public static decimal ReadDecimal(string message)
    {
        bool isRunning = true;
        decimal result = 0;

        while (isRunning)
        {
            Console.Write(message);
            string userValue = Console.ReadLine()?.Replace(',', '.') ?? string.Empty;
 
            if (decimal.TryParse(userValue, CultureInfo.InvariantCulture, out decimal value))
            {
                result = value;
                isRunning = false;
            }
            else
            {
                Console.WriteLine("Ошибка : введите число.");
            }
        }

        return result;
    }

    public static string ReadText(string message)
    {
        bool isRunning = true;
        string result = string.Empty;

        while (isRunning)
        {
            Console.Write(message);
            string text = (Console.ReadLine() ?? string.Empty).Trim();
 
            if (text != string.Empty)
            {
                result = text;
                isRunning = false;
            }
            else
            {
                Console.WriteLine("Ошибка : строка не должна быть пустой.");
            }
        }

        return result;
    }
 
    public static List<int>? ReadNumbers(string message)
    {
        Console.Write(message);
        string[] parts = (Console.ReadLine() ?? "").Split([' ', ',', ';'], StringSplitOptions.RemoveEmptyEntries);
 
        List<int> numbers = new List<int>();
 
        foreach (string part in parts)
        {
            if (!int.TryParse(part, out int value))
            {
                Console.WriteLine($"Ошибка : '{part}' - не число.");
                return null;
            }
 
            numbers.Add(value);
        }
 
        return numbers;
    }

    public static void PrintHeader(string text)
    {
        Console.WriteLine($"\n--- {text} ---");
    }

    public static DateTime ReadDateTimeExact(string message)
    {
        string format = "dd.MM.yyyy"; 
        bool isRunning = true;
        DateTime result = default;

        while (isRunning)
        {
            Console.Write(message);
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
            {
                result = value;
                isRunning = false;
            }
            else
            {
                Console.WriteLine($"Ошибка : дата должна быть строго в формате {format} (например, 28.10.2026).");
            }
        }

        return result;
    }
}
