namespace FileTasks;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Задачи из файла");
        Console.WriteLine("1 - Перемешивание List с парами картинок");
        Console.WriteLine("2 - Студенты : чтение из файла и меню");
        Console.WriteLine("3 - Бабули и больницы (очередь и стек)");
        Console.WriteLine("4 - Обход графа в ширину, кратчайший путь");

        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\nВыберите номер задания (1-4, 0 для выхода) : ");
            
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 4)
            {
                Console.WriteLine("Ошибка : ожидалось целое число от 0 до 4.");
                continue;
            }

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Завершение работы...");
                    isRunning = false;
                    break;
                case 1: Task1(); break;
                case 2: Task2(); break;
                case 3: Task3(); break;
                case 4: Task4(); break;
            }
        }
    }

    // 1 - Перемешивание List с парами картинок
    internal static void Task1()
    {
        Console.WriteLine("1 - Перемешивание List с парами картинок");
        Console.WriteLine("От пользователя ничего не требуется");

        string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "MyImages"); 

        List<string> imagePaths = new List<string>(64);

        try
        {
            string[] extensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            
           List<string> files = Directory.GetFiles(relativePath)
                                  .Where(file => extensions.Contains(Path.GetExtension(file).ToLower()))
                                  .Take(32)
                                  .ToList();

            if (files.Count < 32)
            {
                Console.WriteLine($"Ошибка: в папке найдено {files.Count} картинок, а нужно 32.");
                return;
            }

            foreach (string file in files)
            {
                imagePaths.Add(file);
                imagePaths.Add(file);
            }
            
            Utilites.PrintImagesPaths(imagePaths, "Исходный список");

            Utilites.Shuffle(imagePaths);

            Utilites.PrintImagesPaths(imagePaths, "Перемешанный списко");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Ошибка: Указанная папка не найдена. Проверьте относительный путь.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    // 2 - Студенты : чтение из файла и меню
    internal static void Task2()
    {
        string studentsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "StudentsTask2.txt");

        Console.WriteLine("2 - Студенты : чтение из файла и меню");
        Console.WriteLine("Команды : Новый студент / Удалить / Сортировать / Выход");

        Dictionary<Guid, Student>? students = Utilites.LoadStudents(studentsFilePath);
        if (students == null)
        {
            return;
        }

        Utilites.PrintStudents(students);

        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\nКоманда : ");
            string command = Console.ReadLine()!;

            switch (command)
            {
                case "Новый студент":
                    Utilites.AddNewStudent(students, studentsFilePath);
                    break;

                case "Удалить":
                    Utilites.RemoveStudent(students, studentsFilePath);
                    break;

                case "Сортировать":
                    List<KeyValuePair<Guid, Student>> notSortedStudents = new List<KeyValuePair<Guid, Student>>(students);
                    List<KeyValuePair<Guid, Student>> sortedStudents = notSortedStudents.OrderBy(x => x.Value.Score).ToList();
    
                    Console.WriteLine("Студенты по баллам (по возрастанию) : ");
                    Utilites.PrintStudents(sortedStudents);
                    break;

                case "Выход":
                    Console.WriteLine("Завершение работы...");
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Ошибка: Неизвестная команда. Попробуйте еще раз.");
                    break;
            }
        }
    }

    // 3 - Бабули и больницы (очередь и стек)
    internal static void Task3()
    {
        Console.WriteLine("3 - Бабули и больницы (очередь и стек)");

        Stack<Hospital> hospitals = Utilites.CreateHospitals();

        Queue<Grandmother> queue = new Queue<Grandmother>();
        List<Grandmother> allGrandmothers = new List<Grandmother>();

        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\nИмя бабули (пустой ввод  закончить) : ");
            string name = (Console.ReadLine() ?? "").Trim();
            if (name == "")
            {
                isRunning = false;
                break;
            }

            Console.Write("Дата рождения (ДД.ММ.ГГГГ) : ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime birthDate) && birthDate.Year > DateTime.Now.Year) //Было неверное условие, нужно >
            {
                Console.WriteLine("Ошибка: Некорректный возраст. Бабуля не добавлена.");
                continue;
            }

            Console.Write("Болезни через запятую : ");
            List<string> diseases = Utilites.SplitByComma(Console.ReadLine());

            Console.Write("Лекарства через запятую : ");
            List<string> medicines = Utilites.SplitByComma(Console.ReadLine());

            Grandmother grandmother = new Grandmother(name, birthDate, diseases, medicines);
            queue.Enqueue(grandmother);
            allGrandmothers.Add(grandmother);
        }

        List<Grandmother> onStreetForCry = new List<Grandmother>();

        while (queue.Count > 0)
        {
            Grandmother grandmother = queue.Dequeue();

            if (!Utilites.TryPlaceGrandma(grandmother, hospitals))
            {
                onStreetForCry.Add(grandmother);
            }
        }

        Utilites.PrintGrandmaResults(allGrandmothers, hospitals, onStreetForCry);
    }

    // 4 - Обход графа в ширину, кратчайший путь
    internal static void Task4()
    {
        Console.WriteLine("4 - Обход графа в ширину, кратчайший путь");

        Console.WriteLine("Граф неориентированный, вершины 0-7. Необходимо ввести стартовую и конечную вершины");

        const int vertexCount = 8;

        int[,] edges =
        {
            { 0, 1 }, { 0, 2 }, { 1, 3 }, { 2, 3 }, { 3, 4 },
            { 4, 5 }, { 2, 6 }, { 6, 7 }, { 5, 7 }
        };

        List<int>[] graph = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            graph[i] = new List<int>();
        }

        for (int i = 0; i < edges.GetLength(0); i++)
        {
            int from = edges[i, 0];
            int to = edges[i, 1];
            graph[from].Add(to);
            graph[to].Add(from);
        }

        Console.WriteLine("\nСписок смежности : ");
        for (int i = 0; i < vertexCount; i++)
        {
            Console.WriteLine($"{i} : {string.Join(", ", graph[i])}");
        }

        int start = Utilites.ReadVertex("Стартовая вершина : ", vertexCount);
        int end = Utilites.ReadVertex("Конечная вершина : ", vertexCount);

        List<int> path = Utilites.FindShortestPath(graph, start, end);

        if (path.Count == 0)
        {
            Console.WriteLine("Пути между этими вершинами нет");
        }
        else
        {
            Console.WriteLine($"Кратчайший путь ({path.Count - 1} ребёр): {string.Join(" -> ", path)}");
        }
    }
}
