using MyLibrary.Tumakov;
using MyLibrary.FileTasks;


namespace FileTasks;

class Program
{
    static void Main(string[] args)
    {
        //Задания из файла
        Console.WriteLine("Задания из файла");

        //Выводим все возможные на выбор задания с кратким пояснением
        Console.WriteLine("Задание 1 - поменять местами 2 введённых числа из массива 20 случайных чисел");
        Console.WriteLine("Задание 2 - метод, возвращающий сумму элементов массива(return), произведение массива(ref), среднее арифметическое массива(out)");
        Console.WriteLine("Задание 3 - обработать ввод числа и вывести в консоль псевдографическое (ASCII-арт) представление заданной цифры");
        Console.WriteLine("Задание 4 - ворчливые деды и синяки за лексику от бабки");

        // Запускаем бесконечный цикл, чтобы пользователь мог тестировать разные задачи без перезапуска программы
        while (true)
        {
            Console.Write("Выберите номер необходимого задания(1-4) (0 для завершения работы) : ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice < 5)
            {
                switch (choice)
                {
                    case 0: 
                        Console.WriteLine("Завершение работы...");
                        return;
                    case 1: Task1(); break;
                    case 2: Task2(); break;
                    case 3: Task3(); break;
                    case 4: Task4(); break;
                }
            }    
            else
            {
                Console.WriteLine("\nОшибка : Некорректный ввод. Ожидалось целое число от 0 до 4.");
            }         
        }
    }

    //Задание 1 - поменять местами 2 введённых числа из массива 20 случайных чисел
    internal static void Task1()
    {
        Console.WriteLine("Задание 1 - поменять местами 2 введённых числа из массива 20 случайных чисел");

        int arrayLength = 20;
        int[] arrayNumbers = FileMethods.GenerateRandomArray(arrayLength);

        Console.Write("Исходный массив : ");
        Console.Write(string.Join(", ", arrayNumbers));

        Console.Write("\nВведите первое число из массива : ");
        bool isFirstValid = int.TryParse(Console.ReadLine(), out int firstArrayNumber);
        
        Console.Write("\nВведите второе число из массива : ");
        bool isSecondValid = int.TryParse(Console.ReadLine(), out int secondArrayNumber);

        if (!isFirstValid || !isSecondValid)
        {
            Console.WriteLine("\nОшибка : Некорректный ввод. Ожидалось целое число.");
            return;
        }

        int indexFirstNumber = Array.IndexOf(arrayNumbers, firstArrayNumber);
        int indexSecondNumber = Array.IndexOf(arrayNumbers, secondArrayNumber);

        if (indexFirstNumber == -1 || indexSecondNumber == -1)
        {
            Console.WriteLine("\nОшибка: Одно или оба числа не найдены в массиве.");
            return;
        }

        TumakovMethods.SwapParams(ref arrayNumbers[indexFirstNumber], ref arrayNumbers[indexSecondNumber]);

        Console.WriteLine("\nМассив после обмена элементов:");
        Console.WriteLine(string.Join(", ", arrayNumbers));
    }

    //Задание 2 - метод, возвращающий сумму элементов массива(return), произведение массива(ref), среднее арифметическое массива(out)  
    internal static void Task2()
    {
        Console.WriteLine("Задание 2 - метод, возвращающий сумму элементов массива(return), произведение массива(ref), среднее арифметическое массива(out)");

        double productResult = 0;
        int arrayLength = 20;
        int[] arrayNumbers = FileMethods.GenerateRandomArray(arrayLength);

        int sumResult = FileMethods.ProcessNumbers(out double averageResult, ref productResult, arrayNumbers);

        Console.WriteLine("Массив : " + string.Join(", ", arrayNumbers));
        Console.WriteLine($"Сумма элементов (через return):       {sumResult}");
        Console.WriteLine($"Произведение элементов (через ref): {productResult:E3}");
        Console.WriteLine($"Среднее арифметическое (через out): {averageResult:F2}");
    }

    //Задание 3 - обработать ввод числа и вывести в консоль псевдографическое (ASCII-арт) представление заданной цифры
    internal static void Task3()
    {
        Console.WriteLine("Задание 3 - обработать ввод числа и вывести в консоль псевдографическое (ASCII-арт) представление заданной цифры");

        while (true)
        {
            Console.Write("Введите цифру от 0 до 9 (или 'exit'/'закрыть' для выхода): ");
            string input = Console.ReadLine()!.Trim().ToLower();

            if (input == "exit" || input == "закрыть")
            {
                Console.WriteLine("\nЗавершение работы задания...");
                return;
            }

            if (!int.TryParse(input, out int number))
            {
                throw new FormatException("Ошибка: Введённое значение не является числом.");
            }

            if (number < 0 || number > 9)
            {
                ConsoleColor originalBackground = Console.BackgroundColor;
                ConsoleColor originalForeground = Console.ForegroundColor;

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();

                Console.WriteLine($"Ошибка: Число {number} выходит за пределы диапазона 0-9.");

                Thread.Sleep(3000); 

                Console.BackgroundColor = originalBackground;
                Console.ForegroundColor = originalForeground;
                Console.Clear();
                
                continue;
            }

            FileMethods.DrawDigit(ref number);
        }
    }
    //Задание 4 - ворчливые деды и синяки за лексику от бабки
    internal static void Task4()
    {
        Console.WriteLine("Задание 4 - ворчливые деды и синяки за лексику от бабки");

        Grandfather[] grandfathers = new Grandfather[5];

        grandfathers[0] = new Grandfather("Иваныч", GrumpinessLevel.Low, 
            new string[] {"Не нормально это трезвыми ходить", "ешкин кот", "чорт", "а вот в наше время", "фиг вам"});

        grandfathers[1] = new Grandfather("Петрович", GrumpinessLevel.Medium, 
            new string[] {"нерись", "дьявол бы побрал", "епт", "чурка"});

        grandfathers[2] = new Grandfather("Степаныч", GrumpinessLevel.High, 
            new string[] {"еть", "збс", "суп рататуй - вокруг вода, а в центре Х", "капец"});

        grandfathers[3] = new Grandfather("Михалыч", GrumpinessLevel.Extreme, 
            new string[] {"как мертвого за Х тянуть", "подарок - из П огарок", "руки под Х заточены", "сдуру можно и Х сломать",
                            "последний Х без соли доедать"});

        grandfathers[4] = new Grandfather("Кузьмич", GrumpinessLevel.High, 
            new string[] {"таких друзей за Х и в музей", "то ли лыжи не едут, то ли я того", "Х на рыло, чтобы сердце не ныло",
                            "через Х зари не видно"});

        string[] forbiddenWords = {"рыло", "дуру", "капец", "чорт"};

        Console.WriteLine("--- Проверка дедов на лексику бабкой ---");

        for (int i = 0; i < grandfathers.Length; i++)
        {
            int bruisesReceived = Grandfather.CheckSwearWords(grandfathers[i], forbiddenWords);
            
            grandfathers[i].BruisesCount += bruisesReceived;

            Console.WriteLine($"Дед: {grandfathers[i].Name} (Уровень ворчливости: {grandfathers[i].Grumpiness})");
            Console.WriteLine($"Количество фраз: {grandfathers[i].GrumblePhrases.Length}");
            Console.WriteLine($"Бабка поставила фингалов в этот раз: {bruisesReceived}");
            Console.WriteLine($"Всего синяков у деда: {grandfathers[i].BruisesCount}");
            Console.WriteLine();
        }
    }
}