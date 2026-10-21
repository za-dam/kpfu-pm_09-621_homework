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

    public static void PrintImagesPaths(List<string> list, string title)
    {
        Console.WriteLine($"\n{title}:");

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"№{i+1} - {list[i]}");
        }
    }


// СТУДЕНТЫ


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

    private static void SaveStudents(Dictionary<Guid, Student> students, string studentsFilePath)
    {
        string json = JsonSerializer.Serialize(students, jsonOptions);
        File.WriteAllText(studentsFilePath, json);
    }

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

    private static void AddToDictionary(Dictionary<Guid, Student> students, Student student)
    {
        students.Add(student.Id, student);
    }

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

    public static void PrintStudents(List<KeyValuePair<Guid, Student>> students)
    {
        int number = 1;

        Console.WriteLine();
        foreach (KeyValuePair<Guid, Student> pair in students)
        {
            Console.WriteLine($"{number}. {pair.Value.GetInfo()}");
            number++;
        }
    }


// БАБКИ И БОЛЬНИЦЫ


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