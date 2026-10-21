namespace TumakovTasks;

class Program
{   
    internal static string fileName;

    internal static readonly string[] monthNames =
    {
        "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
        "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
    };

    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            fileName = args[0];
        }

        Console.WriteLine("Лабораторная 6 - массивы и коллекции");
        Console.WriteLine("1 - Упражнение 6.1 : гласные и согласные в файле (массив char[])");
        Console.WriteLine("2 - Упражнение 6.2 : умножение двух матриц (двумерный массив)");
        Console.WriteLine("3 - Упражнение 6.3 : средняя температура по месяцам (массив [12,30])");
        Console.WriteLine("4 - Домашнее задание 6.1 : то же, что 6.1, но через List<char>");
        Console.WriteLine("5 - Домашнее задание 6.2 : то же, что 6.2, но через LinkedList<LinkedList<int>>");
        Console.WriteLine("6 - Домашнее задание 6.3 : то же, что 6.3, но через Dictionary<string, int[]>");
 
        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\nВыберите номер задания (1-6, 0 для выхода) : ");
            
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 6)
            {
                Console.WriteLine("Ошибка : ожидалось целое число от 0 до 6.");
                continue;
            }

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Завершение работы...");
                    isRunning = false;
                    break;
                case 1: Exercise61(); break;
                case 2: Exercise62(); break;
                case 3: Exercise63(); break;
                case 4: Homework61(); break;
                case 5: Homework62(); break;
                case 6: Homework63(); break;
            }
        }
    }

    // Упражнение 6.1. - кол-во гласных и согласных в файле
    internal static void Exercise61()
    {
        Console.WriteLine("Упражнение 6.1. - подсчёт гласных и согласных букв в текстовом файле (через массив символов)");

        string? filePath = Utilites.GetFileName(fileName);
        if (filePath == null)
        {
            return;
        }

        try
        {
            string fileContent = File.ReadAllText(filePath);
            char[] charArray = fileContent.ToCharArray();

            Utilites.CountVowelsAndConsonants(ref charArray, out uint vowels, out uint consonants);

            Console.WriteLine($"Гласных букв: {vowels}");
            Console.WriteLine($"Согласных букв: {consonants}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при чтении файла: {ex.Message}");
        }
    }

    // Упражнение 6.2. - умножение двух матриц (двумерные массивы)
    internal static void Exercise62()
    {
        Console.WriteLine("Упражнение 6.2 - умножение двух матриц (двумерный массив)");
        Console.WriteLine("Умножение двух матриц A (n x m) и B (m x k), заполненных случайными числами");
        Console.WriteLine("Нужно ввести три размера: n, m, k (целые положительные числа)");
        
        int n = Utilites.ReadPositiveInt("Введите кол-во строк в матрице A (n) : ");
        int m = Utilites.ReadPositiveInt("Введите кол-во столбцов в A = строк в B (m) : ");
        int k = Utilites.ReadPositiveInt("Введите кол-во столбцов в матрице B (k) : ");

        int[,] matrixA = Utilites.CreateRandomMatrix(n, m);
        int[,] matrixB = Utilites.CreateRandomMatrix(m, k);

        Utilites.PrintMatrix(matrixA, "Матрица A");
        Utilites.PrintMatrix(matrixB, "Матрица B");

        int[,] result = Utilites.MultiplyMatrices(matrixA, matrixB);
        Utilites.PrintMatrix(result, "Результат A * B");
    }

    // Упражнение 6.3 - средняя температура по месяцам (массив [12,30])
    internal static void Exercise63()
    {
        Console.WriteLine("Упражнение 6.3 - средняя температура по месяцам (массив [12,30])");
        Console.WriteLine("Массив случайных температур за год (12 месяцев по 30 дней), определение средней по каждому месяцу");
        Console.WriteLine("От пользователя ничего не требуется");

        int months = 12;
        int days = 30;

        int[,] temperatures = Utilites.CreateRandomMatrix(months, days);

        double[] averages = Utilites.GetMonthAverages(temperatures);

        Console.WriteLine("\nСредние температуры по месяцам:");
        for (int i = 0; i < averages.Length; i++)
        {
            Console.WriteLine($"{monthNames[i]} - {averages[i]:F2}");
        }

        Array.Sort(averages);

        Console.WriteLine("\nСредние температуры по возрастанию:");
        for (int i = 0; i < averages.Length; i++)
        {
            Console.Write($"{averages[i]:F2} -> ");
        }
    }

    // Домашнее задание 6.1 - то же, что 6.1 (кол-во гласных и согласных в файле), но через List<char>
    internal static void Homework61()
    {
        Console.WriteLine("Домашнее задание 6.1 - то же, что 6.1 (кол-во гласных и согласных в файле), но через List<char>");

        string? filePath = Utilites.GetFileName(fileName);
        if (filePath == null)
        {
            return;
        }

        try
        {
            string fileContent = File.ReadAllText(filePath);
            List<char> charArray = new List<char>(fileContent.ToCharArray());

            Utilites.CountVowelsAndConsonants(ref charArray, out uint vowels, out uint consonants);

            Console.WriteLine($"Гласных букв: {vowels}");
            Console.WriteLine($"Согласных букв: {consonants}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при чтении файла: {ex.Message}");
        }
    }

    // Домашнее задание 6.2 - то же, что 6.2 (умножение двух матриц), но через LinkedList<LinkedList<int>>
    internal static void Homework62()
    {
        Console.WriteLine("Домашнее задание 6.2 - то же, что 6.2 (умножение двух матриц), но через LinkedList<LinkedList<int>>");

        Console.WriteLine("Умножение двух матриц A (n x m) и B (m x k), заполненных случайными числами");
        Console.WriteLine("Нужно ввести три размера: n, m, k (целые положительные числа)");
        
        int n = Utilites.ReadPositiveInt("Введите кол-во строк в матрице A (n) : ");
        int m = Utilites.ReadPositiveInt("Введите кол-во столбцов в A = строк в B (m) : ");
        int k = Utilites.ReadPositiveInt("Введите кол-во столбцов в матрице B (k) : ");

        LinkedList<LinkedList<int>> matrixA = Utilites.CreateRandomLinkedMatrix(n, m);
        LinkedList<LinkedList<int>> matrixB = Utilites.CreateRandomLinkedMatrix(m, k);

        Utilites.PrintMatrix(matrixA, "Матрица A");
        Utilites.PrintMatrix(matrixB, "Матрица B");

        LinkedList<LinkedList<int>> result = Utilites.MultiplyMatrices(matrixA, matrixB);
        Utilites.PrintMatrix(result, "Результат A * B");
    }

    // Домашнее задание 6.3 - то же, что 6.3 (средняя температура по месяцам), но через Dictionary<string, int[]>
    internal static void Homework63()
    {
        Console.WriteLine("Домашнее задание 6.3 - то же, что 6.3 (средняя температура по месяцам), но через Dictionary<string, int[]>");
        Console.WriteLine("Температуры за год в Dictionary<string, int[]>: ключ - месяц, значение - 30 температур");
        Console.WriteLine("От пользователя ничего не требуется");

        Dictionary<string, int[]> temperatures = new Dictionary<string, int[]>();

        foreach (string month in monthNames)
        {
            int[] days = new int[30];
            for (int i = 0; i < days.Length; i++)
            {
                days[i] = Utilites.random.Next(-30, 36);
            }
            temperatures.Add(month, days);
        }

        double[] averages = Utilites.GetMonthAverages(temperatures);

        Console.WriteLine("\nСредние температуры по месяцам:");
        int index = 0;
        foreach (string month in temperatures.Keys)
        {
            Console.WriteLine($"{month} {averages[index]:F2}");
            index++;
        }

        Array.Sort(averages);

        Console.WriteLine("\nСредние температуры по возрастанию:");
        for (int i = 0; i < averages.Length; i++)
        {
            Console.Write($"{averages[i]:F2} -> ");
        }
    }
}
