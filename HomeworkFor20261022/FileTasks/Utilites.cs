using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;


namespace FileTasks;

public static class Utilites
{
    internal static readonly Random rng = new Random();

    internal static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
    {
        IncludeFields = true,
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };


//КАРТИНКИ


    /// <summary>
    /// Перемешивает элементы списка в случайном порядке с помощью алгоритма тасования Фишера — Йетса.
    /// </summary>
    /// <typeparam name="T">Тип элементов, содержащихся в списке.</typeparam>
    /// <param name="list">Список, элементы которого необходимо перемешать.</param>
    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    /// <summary>
    /// Выводит в консоль заголовок и нумерованный список имён файлов (без полных путей) из предоставленного списка.
    /// </summary>
    /// <param name="list">Список строк, содержащих полные или относительные пути к файлам изображений.</param>
    /// <param name="title">Текст заголовка, который будет отображен перед началом списка.</param>
    public static void PrintImagesPaths(List<string> list, string title)
    {
        Console.WriteLine($"\n{title}:");

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"№{i+1} - {Path.GetFileName(list[i])}");
        }
    }


// СТУДЕНТЫ


    /// <summary>
    /// Загружает словарь студентов из JSON-файла. 
    /// Если файл не существует, создаёт его с дефолтными данными (10 студентов) и возвращает их.
    /// </summary>
    /// <param name="studentsFilePath">Путь к файлу в формате JSON, содержащему данные студентов.</param>
    /// <returns>
    /// Возвращает словарь <see cref="Dictionary{Guid, Student}"/> с данными студентов в случае успешной загрузки или создания файла; 
    /// возвращает <see langword="null"/>, если файл поврежден или имеет неверный формат JSON.
    /// </returns>
    public static Dictionary<Guid, Student>? LoadStudents(string studentsFilePath)
    {
        if (!File.Exists(studentsFilePath))
        {
            Dictionary<Guid, Student> defaultStudents = CreateDefaultStudents();
            SaveStudents(defaultStudents, studentsFilePath);
            Console.WriteLine($"Файл {studentsFilePath} не найден - создан со списком из 10 студентов.");
            return defaultStudents;
        }
 
        try
        {
            string json = File.ReadAllText(studentsFilePath);
            return JsonSerializer.Deserialize<Dictionary<Guid, Student>>(json, jsonOptions)
                   ?? new Dictionary<Guid, Student>();
        }
        catch (JsonException)
        {
            Console.WriteLine($"Ошибка: файл {studentsFilePath} повреждён или записан в неверном формате.");
            return null;
        }
    }

    /// <summary>
    /// Сохраняет словарь студентов в файл в формате JSON.
    /// </summary>
    /// <param name="students">Словарь студентов для сериализации.</param>
    /// <param name="studentsFilePath">Путь к файлу, в который будут записаны данные.</param>
    private static void SaveStudents(Dictionary<Guid, Student> students, string studentsFilePath)
    {
        string json = JsonSerializer.Serialize(students, jsonOptions);
        File.WriteAllText(studentsFilePath, json);
    }

    /// <summary>
    /// Создаёт и инициализирует словарь со списком по умолчанию, состоящим из 10 студентов.
    /// </summary>
    /// <returns>Инициализированный словарь <see cref="Dictionary{Guid, Student}"/>, где ключом является уникальный идентификатор студента, а значением — объект <see cref="Student"/>.</returns>
    private static Dictionary<Guid, Student> CreateDefaultStudents()
    {
        Dictionary<Guid, Student> students = new Dictionary<Guid, Student>();
 
        students.Add(Guid.NewGuid(), new Student("Иванов", "Иван", new DateTime(2006, 1, 14), "Математика", 85));
        students.Add(Guid.NewGuid(), new Student("Петров", "Пётр", new DateTime(2005, 8, 12), "Физика", 78));
        students.Add(Guid.NewGuid(), new Student("Сидорова", "Анна", new DateTime(2006, 9, 13), "Информатика", 92));
        students.Add(Guid.NewGuid(), new Student("Кузнецов", "Дмитрий", new DateTime(2006, 6, 22), "Математика", 67));
        students.Add(Guid.NewGuid(), new Student("Смирнова", "Мария", new DateTime(2005, 4, 8), "Русский язык", 74));
        students.Add(Guid.NewGuid(), new Student("Попов", "Алексей", new DateTime(2006, 12, 18), "Информатика", 88));
        students.Add(Guid.NewGuid(), new Student("Васильева", "Ольга", new DateTime(2006, 3, 29), "Физика", 70));
        students.Add(Guid.NewGuid(), new Student("Соколов", "Андрей", new DateTime(2005, 2, 12), "Математика", 95));
        students.Add(Guid.NewGuid(), new Student("Михайлова", "Елена", new DateTime(2006, 11, 5), "Информатика", 81));
        students.Add(Guid.NewGuid(), new Student("Новиков", "Сергей", new DateTime(2006, 12, 11), "Физика", 63));
 
        return students;
    }

    /// <summary>
    /// Запрашивает данные нового студента через консольный ввод, валидирует их и, в случае успеха, добавляет в словарь и сохраняет в файл.
    /// </summary>
    /// <param name="students">Текущий словарь студентов, в который будет добавлена новая запись.</param>
    /// <param name="studentsFilePath">Путь к JSON-файлу для сохранения обновленного списка студентов.</param>
    public static void AddNewStudent(Dictionary<Guid, Student> students, string studentsFilePath)
    {
        Console.Write("Фамилия : ");
        string surname = Console.ReadLine()!;
        Console.Write("Имя : ");
        string name = Console.ReadLine()!;
        Console.Write("Дата рождения (в формате ДД.ММ.ГГГГ) : ");
        bool isDateCorrect = DateTime.TryParse(Console.ReadLine(), out DateTime birthDate) && birthDate.Year <= DateTime.Now.Year;
        Console.Write("С каким экзаменом поступил : ");
        string exam = Console.ReadLine()!;
        Console.Write("Балл за экзамен : ");
        bool isScoreCorrect = byte.TryParse(Console.ReadLine(), out byte score) && score <= 100;

        if (surname == "" || name == "" || !isDateCorrect || !isScoreCorrect)
        {
            Console.WriteLine("Ошибка: данные введены неверно, студент не добавлен.");
            return;
        }

        AddToDictionary(students, new Student(surname, name, birthDate, exam, score));
        SaveStudents(students, studentsFilePath);
        Console.WriteLine("Студент добавлен и сохранён в файл");
    }

    /// <summary>
    /// Запрашивает уникальный идентификатор (GUID) студента через консольный ввод, удаляет соответствующую запись из словаря и обновляет файл данных.
    /// </summary>
    /// <param name="students">Текущий словарь студентов, из которого производится удаление.</param>
    /// <param name="studentsFilePath">Путь к JSON-файлу для сохранения обновленного списка студентов.</param>
    public static void RemoveStudent(Dictionary<Guid, Student> students, string studentsFilePath)
    {
        Console.Write("Id студента (скопируйте из списка) : ");
 
        if (!Guid.TryParse(Console.ReadLine()?.Trim(), out Guid id))
        {
            Console.WriteLine("Ошибка: это не Id (ожидался вид xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
            return;
        }

        if (students.Remove(id))
        {
            SaveStudents(students, studentsFilePath);
            Console.WriteLine("Студент удалён и файл обновлён");
        }
        else
        {
            Console.WriteLine("Студента с таким Id нет");
        }
    }

    /// <summary>
    /// Вспомогательный метод для добавления студента в словарь, использующий уникальный идентификатор (Id) студента в качестве ключа.
    /// </summary>
    /// <param name="students">Словарь студентов, в который производится добавление.</param>
    /// <param name="student">Объект студента для добавления.</param>
    private static void AddToDictionary(Dictionary<Guid, Student> students, Student student)
    {
        students.Add(student.Id, student);
    }

    /// <summary>
    /// Выводит в консоль нумерованный список студентов из словаря, используя метод получения информации об объекте.
    /// </summary>
    /// <param name="students">Словарь студентов для отображения.</param>
    public static void PrintStudents(Dictionary<Guid, Student> students)
    {
        int number = 1;
 
        Console.WriteLine();
        foreach (KeyValuePair<Guid, Student> pair in students)
        {
            Console.WriteLine($"{number}. {pair.Value.GetInfo()}");
            number++;
        }
    }

    /// <summary>
    /// Выводит в консоль нумерованный список студентов из списка пар "ключ-значение". Используется для отображения отсортированных коллекций.
    /// </summary>
    /// <param name="students">Список пар, содержащих идентификаторы и объекты студентов.</param>
    public static void PrintStudents(List<KeyValuePair<Guid, Student>> students)
    {
        int number = 1;

        Console.WriteLine();
        foreach (KeyValuePair<Guid, Student> pair in students)
        {
            Console.WriteLine($"{number}.  {pair.Value.GetInfo()}");
            number++;
        }
    }


// БАБКИ И БОЛЬНИЦЫ


    /// <summary>
    /// Создаёт и возвращает стек больниц с предустановленными характеристиками (название, профильные болезни, вместимость).
    /// </summary>
    /// <returns>Стек объектов <see cref="Hospital"/>, готовый к заполнению пациентами.</returns>
    public static Stack<Hospital> CreateHospitals()
    {
        Hospital[] list =
        {
            new Hospital("Больница №1 (терапия)", new List<string> { "простуда", "грипп", "давление" }, 2),
            new Hospital("Больница №2 (кардиология)", new List<string> { "давление", "сердце", "аритмия" }, 2),
            new Hospital("Больница №3 (общая)", new List<string> { "диабет", "артрит", "простуда", "сердце" }, 3)
        };

        Stack<Hospital> hospitals = new Stack<Hospital>();

        for (int i = list.Length - 1; i >= 0; i--)
        {
            hospitals.Push(list[i]);
        }

        return hospitals;
    }

    /// <summary>
    /// Разделяет переданную строку по запятым, удаляя лишние пробелы по краям и отсекая пустые элементы.
    /// </summary>
    /// <param name="line">Входная строка для разделения.</param>
    /// <returns>Список очищенных от пробелов подстрок. Если строка пуста, возвращается пустой список.</returns>
    public static List<string> SplitByComma(string? line)
    {
        List<string> result = new List<string>();

        if (string.IsNullOrWhiteSpace(line))
        {
            return result;
        }

        foreach (string part in line.Split(','))
        {
            string item = part.Trim();
            if (item != "")
            {
                result.Add(item);
            }
        }

        return result;
    }

    /// <summary>
    /// Пытается распределить бабушку в первую подходящую больницу из стека на основе наличия свободных мест и уровня лечения её болезней.
    /// </summary>
    /// <param name="grandmother">Объект распределяемой бабушки.</param>
    /// <param name="hospitals">Текущий доступный стек больниц.</param>
    /// <returns>Возвращает <see langword="true"/>, если бабушка успешно госпитализирована; иначе <see langword="false"/>.</returns>
    public static bool TryPlaceGrandma(Grandmother grandmother, Stack<Hospital> hospitals)
    {
        foreach (Hospital hospital in hospitals)
        {
            if (!hospital.HasFreePlace())
            {
                continue;
            }

            if (grandmother.Diseases.Count == 0)
            {
                hospital.Patients.Add(grandmother);
                return true;
            }

            int treated = 0;
            foreach (string disease in grandmother.Diseases)
            {
                if (hospital.TreatedDiseases.Contains(disease, StringComparer.OrdinalIgnoreCase))
                {
                    treated++;
                }
            }

            if (treated * 2 > grandmother.Diseases.Count)
            {
                hospital.Patients.Add(grandmother);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Вычисляет полный возраст на текущую дату на основе переданной даты рождения.
    /// </summary>
    /// <param name="birthDate">Дата рождения.</param>
    /// <returns>Количество полных лет.</returns>
    public static int CalculateAge(DateTime birthDate)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    /// <summary>
    /// Выводит в консоль сводную статистику по симуляции: общий список бабушек, состояние наполненности больниц и список тех, кому не хватило места.
    /// </summary>
    /// <param name="allGrandmothers">Список всех зарегистрированных бабушек.</param>
    /// <param name="hospitals">Стек больниц с данными об их заполнении.</param>
    /// <param name="onStreet">Список бабушек, которые не смогли попасть ни в одну больницу.</param>
    public static void PrintGrandmaResults(List<Grandmother> allGrandmothers, Stack<Hospital> hospitals, List<Grandmother> onStreet)
    {
        Console.WriteLine("\nВсе бабули : ");
        if (allGrandmothers.Count == 0)
        {
            Console.WriteLine("Бабуль нет");
        }
        foreach (Grandmother grandmother in allGrandmothers)
        {
            string diseases = grandmother.Diseases.Count > 0 ? string.Join(", ", grandmother.Diseases) : "нет";
            string medicines = grandmother.Medicines.Count > 0 ? string.Join(", ", grandmother.Medicines) : "нет";
            Console.WriteLine($"Имя : {grandmother.Name}. Возраст : {CalculateAge(grandmother.BirthDate)}. Болезни : {diseases}. Лекарства : {medicines}");
        }

        Console.WriteLine("\nБольницы : ");
        foreach (Hospital hospital in hospitals)
        {
            Console.WriteLine($"{hospital.Name}");
            Console.WriteLine($"Лечат : {string.Join(", ", hospital.TreatedDiseases)}");
            Console.WriteLine($"Бабуль сейчас : {hospital.Patients.Count} из {hospital.Capacity}");
            Console.WriteLine($"Заполненность : {hospital.FillPercent():F1}%");

            foreach (Grandmother patient in hospital.Patients)
            {
                Console.WriteLine($"- {patient.Name}");
            }
        }

        Console.WriteLine("\nНа улице плачут : ");
        if (onStreet.Count == 0)
        {
            Console.WriteLine("Никого");
        }
        foreach (Grandmother grandmother in onStreet)
        {
            Console.WriteLine($"{grandmother.Name}");
        }
    }


// ГРАФЫ И ОБХОД В ШИРИНУ


    /// <summary>
    /// Зацикливает консольный ввод для безопасного считывания индекса вершины графа в валидном диапазоне.
    /// </summary>
    /// <param name="message">Сообщение-приглашение для ввода в консоли.</param>
    /// <param name="vertexCount">Общее количество вершин в графе (для проверки верхней границы).</param>
    /// <returns>Валидный индекс вершины от 0 до (vertexCount - 1).</returns>
    public static int ReadVertex(string message, int vertexCount)
    {
        bool isRunning = true;

        int vertex = 0;

        while (isRunning)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out vertex) && vertex >= 0 && vertex < vertexCount)
            {
                isRunning = false;
                break;
            }
            Console.WriteLine($"Ошибка: нужно целое число от 0 до {vertexCount - 1}.");
        }

        return vertex;
    }

    /// <summary>
    /// Находит кратчайший путь между двумя вершинами графа с помощью алгоритма поиска в ширину (BFS).
    /// </summary>
    /// <param name="graph">Представление графа в виде массива списков смежности.</param>
    /// <param name="start">Индекс начальной вершины.</param>
    /// <param name="end">Индекс конечной вершины.</param>
    /// <returns>Список индексов вершин, последовательно формирующих кратчайший путь от старта к финишу. Если пути не существует, возвращается пустой список.</returns>
    public static List<int> FindShortestPath(List<int>[] graph, int start, int end)
    {
        int[] previous = new int[graph.Length];
        bool[] visited = new bool[graph.Length];
        Array.Fill(previous, -1);

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);
        visited[start] = true;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            if (current == end)
            {
                break;
            }

            foreach (int neighbor in graph[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    previous[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        List<int> path = new List<int>();

        if (!visited[end])
        {
            return path;
        }

        int currentVertex = end;

        while (currentVertex != -1)
        {
            path.Add(currentVertex);
            currentVertex = previous[currentVertex];
        }
        
        path.Reverse();

        return path;
    }
}