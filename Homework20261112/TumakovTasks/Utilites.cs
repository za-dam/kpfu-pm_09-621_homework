using System.Globalization;
using System.Numerics; 


namespace TumakovTasks;

public static class Utilites
{
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


    public static string ReverseString(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }

        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);

        return new string(charArray);
    }


    public static bool CheckInterfaceWithIs(object parameter)
    {
        if (parameter is IFormattable)
        {
            return true;
        }
        
        return false;
    }


    public static bool CheckInterfaceWithAs(object parameter)
    {
        IFormattable formattable = parameter as IFormattable;

        if (formattable != null)
        {
            return true;
        }

        return false;
    }


    public static void SearchMail(ref string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return;
        }

        int hashIndex = s.IndexOf('#');
        if (hashIndex == -1)
        {
            return;
        }

        int atIndex = s.IndexOf('@', hashIndex);
        if (atIndex == -1)
        {
            s = string.Empty;
            return;
        }

        bool IsValidEmailChar(char c)
        {
            return char.IsLetterOrDigit(c) || c == '.' || c == '-' || c == '_' || c == '+';
        }

        int leftIndex = atIndex - 1;
        while (leftIndex > hashIndex && IsValidEmailChar(s[leftIndex]))
        {
            leftIndex--;
        }
        leftIndex++;

        int rightIndex = atIndex + 1;
        while (rightIndex < s.Length && IsValidEmailChar(s[rightIndex]))
        {
            rightIndex++;
        }

        int emailLength = rightIndex - leftIndex;
        if (emailLength > 0 && s.Contains("@"))
        {
            s = s.Substring(leftIndex, emailLength);
        }
        else
        {
            s = string.Empty;
        }
    }
}
