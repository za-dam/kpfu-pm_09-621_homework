using static TumakovTasks.Utilites;
using static TumakovTasks.UtilitesForSongs;


namespace TumakovTasks;

class Program
{
    static void Main(string[] args)
    {
        // Задания из Лабораторной Тумакова 8 главы

        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\nЗадания из методички Тумакова - Лабораторная 8 главы");
            Console.WriteLine("Task 1 - Упражнение 8.1. (Банковский счёт и работа с ним)");
            Console.WriteLine("Task 2 - Упражнение 8.2. (Метод переворачивающий строку + тестирование метода)");
            Console.WriteLine("Task 3 - Упражнение 8.3. (Копирование содержимого файла, при наличие, в выходной, но заглавными буквами)");
            Console.WriteLine("Task 4 - Упражнение 8.4. (Метод, который проверяет реализует ли входной параметр метода интерфейс System.IFormattable)");
            Console.WriteLine("Task 5 - Домашнее задание 8.1. (Работа с файлом с информацией людей, формирование нового файла только с почтами)");
            Console.WriteLine("Task 6 - Домашнее задание 8.2. (Список песен, автосвязка с предыдущей после добавления новой, сравнение песен из списка)");

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
                case 1: Exercise81(); break;
                case 2: Exercise82(); break;
                case 3: Exercise83(); break;
                case 4: Exercise84(); break;
                case 5: Homework81(); break;
                case 6: Homework82(); break;
            }
        }
    }

    // Task 1 - Упражнение 8.1. (Банковский счёт и работа с ним)
    internal static void Exercise81()
    {
        Console.WriteLine("Task 1 - Упражнение 8.1. (Банковский счёт и работа с ним)");
        Console.WriteLine("От пользователя требуется ввести баланс счёта и его тип, а после получить информацию и попытаться положить и снять с счёта, а после перевести на чужой счёт");

        Console.Write("Введите тип счёта (Current/Savings) : ");
        AccountType accountType = Console.ReadLine()?.Trim().ToLower() switch
        {
            "current" => AccountType.Current,
            "savings" => AccountType.Savings,
            _ => throw new ArgumentException("Ошибка : Неверный тип счёта. Допустимы только Current или Savings.")
        };

        decimal balance = ReadPositiveNumber<decimal>("Введите баланс счёта (ожидается положительное число) : ");

        BankAccount userAccount = new BankAccount(balance, accountType);

        userAccount.PrintInfo();

        Console.WriteLine("Попробуйте пополнить счёт");
        decimal deposit = ReadPositiveNumber<decimal>("Введите сумму пополнения : ");
        if (!userAccount.TryDeposit(deposit))
        {
            Console.WriteLine("Сумма пополнения не может быть отрицательной");
        }
        Console.WriteLine($"Текущий баланс : {userAccount.Balance:F2}");

        Console.WriteLine("Попробуйте снять со счёта");
        decimal withdraw = ReadPositiveNumber<decimal>("Введите сумму снятия : ");
        if (!userAccount.TryWithdraw(withdraw))
        {
            Console.WriteLine("На счёте недостаточно средств");
        }
        Console.WriteLine($"Текущий баланс : {userAccount.Balance:F2}");

        decimal demoAccountBalance = 1000m;
        AccountType demoAccountType = AccountType.Current;
        decimal demoTransferAmount = 500m;
        BankAccount demoAccount = new BankAccount(demoAccountBalance, demoAccountType);

        Console.WriteLine($"Демонстрационный счёт, который будет переводить сумму - {demoTransferAmount}, на счёт пользователя");
        demoAccount.PrintInfo();

        userAccount.TryMoneyTransfer(demoAccount, demoTransferAmount);

        demoAccount.PrintInfo();
        userAccount.PrintInfo();
    }


    // Task 2 - Упражнение 8.2. (Метод переворачивающий строку + тестирование метода)
    internal static void Exercise82()
    {
        Console.WriteLine("Task 2 - Упражнение 8.2. (Метод переворачивающий строку + тестирование метода)");
        Console.WriteLine("От пользователя требуется ввести строку и посмотреть на результат метода");

        Console.Write("Введите любую непустую строку : ");
        string userInput = Console.ReadLine()!;
        
        ReverseString(userInput);
        
        // Тесты в Homework20261112\TumakovTasks.Tests
    }


    // Task 3 - Упражнение 8.3. (Копирование содержимого файла, при наличие, в выходной, но заглавными буквами)
    internal static void Exercise83()
    {
        Console.WriteLine("Task 3 - Упражнение 8.3. (Копирование содержимого файла, при наличие, в выходной, но заглавными буквами)");
        Console.WriteLine("От пользователя требуется ввести имя файла");

        string resourcesFolder = Path.Combine(AppContext.BaseDirectory, "Resources");

        Console.Write("Введите имя файла для поиска (без расширения) : ");
        string fileName = Console.ReadLine()?.Trim()!;

        if (string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine("Ошибка: Имя файла не может быть пустым.");
            return;
        }

        try
        {
            if (!Directory.Exists(resourcesFolder))
            {
                Console.WriteLine($"Ошибка: Папка '{resourcesFolder}' не найдена.");
                return;
            }

            var foundFiles = Directory.EnumerateFiles(resourcesFolder, $"{fileName}.*").ToList();

            if (foundFiles.Count == 0)
            {
                Console.WriteLine("Такого файла не существует. Завершение работы...");
                return;
            }

            string sourceFilePath = foundFiles[0];

            string fileExtension = Path.GetExtension(sourceFilePath);

            string pathOTNewFile = Path.Combine(resourcesFolder, $"{fileName}CAPS{fileExtension}");

            string fileContent = File.ReadAllText(sourceFilePath);
            string upperFileContent = fileContent.ToUpper();

            File.WriteAllText(pathOTNewFile, upperFileContent);

            Console.WriteLine($"Создан файл с копией содержимого с заглавными буквами : {pathOTNewFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
        }
    }


    // Task 4 - Упражнение 8.4. (Метод, который проверяет реализует ли входной параметр метода интерфейс System.IFormattable)
    internal static void Exercise84()
    {
        Console.WriteLine("Task 4 - Упражнение 8.4. (Метод, который проверяет реализует ли входной параметр метода интерфейс System.IFormattable)");
        Console.WriteLine("От пользователя ничего не требуется, будут продемонстрированы 2 типа, остальные типы в тестах");

        var testDateTime = DateTime.Now;
        var testArray = new int[] { 1, 2, 3 };

        Console.WriteLine("Проверка метода с оператором 'is' : ");
        Console.WriteLine($"DateTime : {CheckInterfaceWithIs(testDateTime)}");
        Console.WriteLine($"Array :    {CheckInterfaceWithIs(testArray)}");

        Console.WriteLine("\n--- Проверка метода с оператором 'as' : ");
        Console.WriteLine($"DateTime : {CheckInterfaceWithAs(testDateTime)}");
        Console.WriteLine($"Array :    {CheckInterfaceWithAs(testArray)}"); 

        // Для дополнительной демонстрации метода были написаны тесты для 'as' и 'is' 
        // Тесты в Homework20261112\TumakovTasks.Tests   
    }


    // Task 5 - Домашнее задание 8.1. (Работа с файлом, формирование нового файла)
    internal static void Homework81()
    {
        Console.WriteLine("Task 5 - Домашнее задание 8.1. (Работа с файлом, формирование нового файла)");

        string resourcesFolder = Path.Combine(AppContext.BaseDirectory, "Resources");
        string sourceFile = Path.Combine(resourcesFolder, "users.txt");
        string mailLogFile = Path.Combine(resourcesFolder, "emails_only.txt");

        Console.WriteLine($"""
        От пользователя ничего не требуется, при желание пользователь может изменить файл по пути :
        {sourceFile}
        """);

        try
        {
            if (!File.Exists(sourceFile))
            {
                Console.WriteLine($"""
                Ошибка: Исходный файл не найден по пути : 
                {sourceFile}
                """);
                Console.WriteLine($"""
                Создаём файл по пути :
                {sourceFile}
                """);

                File.Create(sourceFile).Close();
                
                Console.WriteLine("Пожалуйста, заполните созданный файл данными (ФИО # email) и запустите программу снова");
                return;
            }

            string[] lines = File.ReadAllLines(sourceFile);
            string[] emails = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];

                SearchMail(ref currentLine);
                
                emails[i] = currentLine;
            }

            File.WriteAllLines(mailLogFile, emails);

            Console.WriteLine($"""
            Список почт сохранен по пути : 
            {mailLogFile}
            """);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Ошибка работы с файлом: {ex.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Ошибка: Нет прав доступа к созданию или чтению файлов в этой папке.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
        }

        // В дополнение были написаны тесты для демонстрации работы метода выделения почты из строки
        // Тесты в Homework20261112\TumakovTasks.Tests   
    }


    // Task 6 - Домашнее задание 8.2. (Список песен, автосвязка с предыдущей после добавления новой, сравнение песен из списка)
    internal static void Homework82()
    {
        Console.WriteLine("Task 6 - Домашнее задание 8.2. (Список песен, автосвязка с предыдущей после добавления новой, сравнение песен из списка)");
        Console.WriteLine("Пользователю предлагается просмотреть работу класса песни и при желание заполнить свой плейлист и сравнить любые 2 песни");

        List<Song> playlist = new List<Song>();

        playlist.Add(new Song("Кукушка", "Кино"));
        playlist.Add(new Song("Группа крови", "Кино"));
        playlist.Add(new Song("Лесник", "Король и Шут"));
        playlist.Add(new Song("Кукушка", "Кино"));

        PrintPlaylist(playlist);
        CheckBinding(playlist);

        bool isResponseValid = false;

        while (!isResponseValid)
        {
            Console.Write("\nХотите заполнить свой плейлист и сравнить любые 2 песни? (yes/no) : ");
            string answer = Console.ReadLine()?.Trim().ToLower()!;

            switch (answer)
            {
                case "yes":
                    isResponseValid = true;
                    playlist.Clear(); 
                    FillUserPlaylist(playlist);
                    PrintPlaylist(playlist);
                    CompareTwoSongs(playlist);
                    break;

                case "no":
                    isResponseValid = true;
                    Console.WriteLine("Выход из задания...");
                    break;

                default:
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите 'yes' или 'no'.");
                    break;
            }
        }
    }
}
