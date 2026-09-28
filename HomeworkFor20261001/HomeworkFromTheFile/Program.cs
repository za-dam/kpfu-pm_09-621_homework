using MyLibrary.File;

namespace HomeworkFromTheFile;

class Program
{
    static void Main(string[] args)
    {
        //Задачи из файла
        Console.WriteLine("Задачи из файла");
        Console.ReadKey();

        //Задача 1 - проверка упорядоченности последовательности из 10 чисел
        Console.WriteLine("Задача 1 - проверка упорядоченности последовательности из 10 чисел");
        Console.WriteLine("Введите 10 целых чисел, каждое с новой строки");
        int[] numbers = SequenceMethods.GenerateSequence(10);
        int resultIncrease = SequenceMethods.CheckSequenceIncrease(numbers);
        if (resultIncrease == -1)
            Console.WriteLine($"Последовательность {numbers} упорядочена по возрастанию");
        else
            Console.WriteLine($"""
            Последовательность {numbers} не упорядочена по возрастанию,
            первое число нарушающее возрастание последовательности {numbers[resultIncrease]} под номером №{resultIncrease+1}
            """);
        Console.ReadKey();

        //Задача 2 - по номеру карты k (6 <= k <= 14) определить её достоинство
        Console.WriteLine("Задача 2 - по номеру карты k (6 <= k <= 14) определить её достоинство");
        Console.Write("Введите целое число от 6 до 14, k = ");
        try
        {
            int cardNumber = int.Parse(Console.ReadLine()!);

            if (cardNumber < 6 || cardNumber > 14)
                throw new ArgumentOutOfRangeException(nameof(cardNumber), cardNumber, "Номер карты вне диапазона");

            Console.WriteLine($"Достоинство карты: {CardValue.GetCardValue(cardNumber)}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: нужно ввести целое число");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Ошибка: слишком большое число");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Ошибка: cardNumber должен быть в диапазоне от 6 до 14");
        }
        finally
        {
            Console.WriteLine("Спасибо за попытку");
        }
        Console.ReadKey();

        //Задача 3 - по профессии вывести подходящий напиток
        Console.WriteLine("Задача 3 - по профессии вывести подходящий напиток");
        Console.Write("Введите название профессии : ");
        string profession = Console.ReadLine()!;
        Console.WriteLine($"Для {profession} напиток {DrinkByProfession.GetDrink(profession)}");
        Console.ReadKey();

        //Задача 4 - по порядковому номеру дня недели (1-7) вывести его название
        Console.WriteLine("Задача 4 - по порядковому номеру дня недели (1-7) вывести его название");
        Console.Write("Введите номер дня недели : ");
        try
        {
            int dayNumber = int.Parse(Console.ReadLine()!);
            if (dayNumber > 7 || dayNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(dayNumber), dayNumber, "");
            WeekDay weekDay = (WeekDay)dayNumber;
            Console.WriteLine($"День недели : {weekDay}");
        }
        catch (ArgumentOutOfRangeException)
        {
             Console.WriteLine("Ошибка: dayNumber должен быть в диапазоне от 1 до 7");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: нужно ввести целое число");
        }
        Console.ReadKey();

        //Задача 5 - обойти массив строк через foreach и посчитать, сколько кукол
        Console.WriteLine("Задача 5 - обойти массив строк через foreach и посчитать, сколько кукол");
        string[] items =
        {
            "Hello Kitty", "Lego", "Barbie doll", "Toy car",
            "Hello Kitty", "Teddy bear", "Barbie doll", "Ball"
        };
        Console.WriteLine($"Кукол в сумке: {CountDolls.GetCountDolls(items)}");
        Console.ReadKey();
    }
}