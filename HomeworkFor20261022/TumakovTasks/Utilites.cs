namespace TumakovTasks;

public static class Utilites
{
    static readonly Random random = new Random();

    /// <summary>
    /// Получает и валидирует путь к файлу. 
    /// Если переданный аргумент пуст, запрашивает ввод пути у пользователя через консоль.
    /// Проверяет существование файла на диске.
    /// </summary>
    public static string? GetFileName(string? fileName)
    {
        string? path = fileName;

        if (string.IsNullOrWhiteSpace(path))
        {
            Console.Write("Аргумент с именем файла не передан. Введите путь к файлу : ");
            path = Console.ReadLine();
        }

        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
        {
            Console.WriteLine("Ошибка: файл не найден.");
            return null;
        }

        return path;
    }

    /// <summary>
    /// Метод вычисляет и записывает по ссылке количество гласных и согласных букв в массиве символов
    /// </summary>
    public static void CountVowelsAndConsonants(ref char[] chars, out uint vowelsCount, out uint consonantsCount)
    {
        string vowelsLookUp = "аеёиоуыэюяaeiouy";
        string consonantsLookUp = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxz";

        vowelsCount = 0;
        consonantsCount = 0;

        foreach (char symbol in chars)
        {
            if (!char.IsLetter(symbol))
            {
                continue;
            }

            char lowerSymbol = char.ToLower(symbol);

            if (vowelsLookUp.Contains(lowerSymbol))
            {
                vowelsCount++;
            }
            else if (consonantsLookUp.Contains(lowerSymbol))
            {
                consonantsCount++;
            }
        }
    }

    /// <summary>
    /// Запрашивает у пользователя ввод целого положительного числа через консоль.
    /// Повторяет запрос в цикле с использованием флага до тех пор, пока не будет введено корректное значение.
    /// </summary>
    public static int ReadPositiveInt(string message)
    {
        int result = 0;
        bool isRunning = true;

        while (isRunning)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
            {
                isRunning = false;
                result =  value;
            }
            Console.WriteLine("Ошибка: нужно целое положительное число.");
        }

        return result;
    }

    /// <summary>
    /// Создает двумерный массив (матрицу) заданного размера и заполняет его 
    /// случайными целыми числами в диапазоне от 1 до 9 включительно.
    /// </summary>
    /// <param name="rows">Количество строк в создаваемой матрице.</param>
    /// <param name="columns">Количество столбцов в создаваемой матрице.</param>
    /// <returns>
    /// Заполненный случайными числами двумерный массив целых чисел размера <paramref name="rows"/> на <paramref name="columns"/>.
    /// </returns>
    public static int[,] CreateRandomMatrix(int rows, int columns)
    {
        int[,] matrix = new int[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = random.Next(-10, 10);
            }
        }

        return matrix;
    }

    /// <summary>
    /// Выводит двумерный массив (матрицу) в консоль в виде форматированной таблицы с заголовком.
    /// Каждое число выравнивается по правому краю в текстовом поле фиксированной ширины.
    /// </summary>
    /// <param name="matrix">Двумерный массив целых чисел, который необходимо напечатать.</param>
    /// <param name="title">Текстовый заголовок, отображаемый перед выводом матрицы.</param>
    public static void PrintMatrix(int[,] matrix, string title)
    {
        Console.WriteLine($"\n{title}:");

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"{matrix[i, j],4}");
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Выполняет математическое умножение двух двумерных массивов (матриц) по правилу «строка на столбец».
    /// </summary>
    /// <param name="matrixA">Первая (левая) матрица множитель.</param>
    /// <param name="matrixB">Вторая (правая) матрица множитель.</param>
    /// <returns>
    /// Новую матрицу, являющуюся результатом произведения <paramref name="matrixA"/> и <paramref name="matrixB"/>.
    /// </returns>
    public static int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
    {
        int rows = matrixA.GetLength(0);
        int common = matrixA.GetLength(1);
        int columns = matrixB.GetLength(1);

        int[,] result = new int[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int sum = 0;
                for (int t = 0; t < common; t++)
                {
                    sum += matrixA[i, t] * matrixB[t, j];
                }
                result[i, j] = sum;
            }
        }

        return result;
    }

    /// <summary>
    /// Вычисляет среднюю температуру для каждого месяца на основе двумерного массива температур.
    /// </summary>
    /// <param name="temperature">
    /// Двумерный массив целых чисел, где строки соответствуют месяцам, 
    /// а столбцы — дням в этих месяцах.
    /// </param>
    /// <returns>
    /// Одномерный массив вещественных чисел (<see cref="double"/>), 
    /// содержащий среднемесячные значения температур.
    /// </returns>
    public static double[] GetMonthAverages(int[,] temperature)
    {
        int months = temperature.GetLength(0);
        int days = temperature.GetLength(1);
        double[] averages = new double[months];

        for (int i = 0; i < months; i++)
        {
            double sum = 0;
            for (int j = 0; j < days; j++)
            {
                sum += temperature[i, j];
            }
            averages[i] = sum / days;
        }

        return averages;
    }

    /// <summary>
    /// Вычисляет количество гласных и согласных букв в списке символов.
    /// Игнорирует знаки препинания, цифры, пробелы и другие небуквенные символы.
    /// </summary>
    /// <param name="chars">Ссылка (<see langword="ref"/>) на динамический список символов, в котором производится подсчет.</param>
    /// <param name="vowelsCount">Выходной параметр (<see langword="out"/>) для возврата общего количества найденных гласных букв.</param>
    /// <param name="consonantsCount">Выходной параметр (<see langword="out"/>) для возврата общего количества найденных согласных букв.</param>
    public static void CountVowelsAndConsonants(ref List<char> chars, out uint vowelsCount, out uint consonantsCount)
    {
        string vowelsLookUp = "аеёиоуыэюяaeiouy";
        string consonantsLookUp = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxz";

        vowelsCount = 0;
        consonantsCount = 0;

        foreach (char symbol in chars)
        {
            if (!char.IsLetter(symbol))
            {
                continue;
            }

            char lowerSymbol = char.ToLower(symbol);

            if (vowelsLookUp.Contains(lowerSymbol))
            {
                vowelsCount++;
            }
            else if (consonantsLookUp.Contains(lowerSymbol))
            {
                consonantsCount++;
            }
        }
    }

    /// <summary>
    /// Создает двумерную структуру данных (матрицу) на основе вложенных связных списков
    /// и заполняет её случайными целыми числами в диапазоне от 1 до 9 включительно.
    /// </summary>
    /// <param name="rows">Количество строк (внутренних списков) в создаваемой структуре.</param>
    /// <param name="columns">Количество столбцов (элементов в каждом внутреннем списке) в создаваемой структуре.</param>
    /// <returns>
    /// Объект <see cref="LinkedList{T}"/>, содержащий цепочку из <paramref name="rows"/> внутренних связных списков, 
    /// каждый из которых заполнен случайными числами и имеет длину <paramref name="columns"/>.
    /// </returns>
    public static LinkedList<LinkedList<int>> CreateRandomLinkedMatrix(int rows, int columns)
    {
        LinkedList<LinkedList<int>> matrix = new LinkedList<LinkedList<int>>();

        for (int i = 0; i < rows; i++)
        {
            LinkedList<int> row = new LinkedList<int>();
            for (int j = 0; j < columns; j++)
            {
                row.AddLast(random.Next(-10, 10));
            }
            matrix.AddLast(row);
        }

        return matrix;
    }

    /// <summary>
    /// Выводит двумерную структуру данных на основе вложенных связных списков в консоль в виде форматированной таблицы с заголовком.
    /// Каждое число выравнивается по правому краю в текстовом поле фиксированной ширины (6 символов).
    /// </summary>
    /// <param name="matrix">Двумерная структура <see cref="LinkedList{T}"/>, содержащая списки-строки целых чисел для вывода.</param>
    /// <param name="title">Текстовый заголовок, отображаемый перед выводом матрицы.</param>
    public static void PrintMatrix(LinkedList<LinkedList<int>> matrix, string title)
    {
        Console.WriteLine($"\n{title}:");

        foreach (LinkedList<int> row in matrix)
        {
            foreach (int value in row)
            {
                Console.Write($"{value,6}");
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Выполняет математическое умножение двух матриц, представленных в виде вложенных связных списков,
    /// по классическому правилу «строка на столбец».
    /// </summary>
    /// <param name="matrixA">Первая (левая) матрица-множитель типа <see cref="LinkedList{T}"/>.</param>
    /// <param name="matrixB">Вторая (правая) матрица-множитель типа <see cref="LinkedList{T}"/>.</param>
    /// <returns>
    /// Новую двумерную структуру <see cref="LinkedList{T}"/>, содержащую результат произведения матриц.
    /// </returns>
    public static LinkedList<LinkedList<int>> MultiplyMatrices(LinkedList<LinkedList<int>> matrixA, LinkedList<LinkedList<int>> matrixB)
    {
        LinkedList<LinkedList<int>> result = new LinkedList<LinkedList<int>>();
        int columnsB = matrixB.First!.Value.Count;

        foreach (LinkedList<int> rowA in matrixA)
        {
            LinkedList<int> resultRow = new LinkedList<int>();

            for (int j = 0; j < columnsB; j++)
            {
                int sum = 0;

                LinkedListNode<int>? nodeA = rowA.First;
                foreach (LinkedList<int> rowB in matrixB)
                {
                    sum += nodeA!.Value * GetElementAt(rowB, j);
                    nodeA = nodeA.Next;
                }

                resultRow.AddLast(sum);
            }

            result.AddLast(resultRow);
        }

        return result;
    }

    /// <summary>
    /// Вспомогательный метод для имитации индексного доступа в связном списке.
    /// Находит и возвращает значение узла по его порядковому номеру, перебирая элементы с начала списка.
    /// </summary>
    /// <param name="list">Связный список целых чисел <see cref="LinkedList{T}"/>, в котором ищется элемент.</param>
    /// <param name="index">Индекс (позиция) искомого элемента, начиная с 0.</param>
    /// <returns>Целочисленное значение ячейки, находящейся под указанным индексом.</returns>
    public static int GetElementAt(LinkedList<int> list, int index)
    {
        LinkedListNode<int>? node = list.First;
        for (int i = 0; i < index; i++)
        {
            node = node!.Next;
        }
        return node!.Value;
    }

    /// <summary>
    /// Вычисляет среднюю температуру для каждого месяца на основе данных из словаря.
    /// </summary>
    /// <param name="temperature">
    /// Словарь, где ключом (<see cref="string"/>) является название месяца, 
    /// а значением (<see cref="int"/>[]) — массив ежедневных температур.
    /// </param>
    /// <returns>
    /// Одномерный массив вещественных чисел (<see cref="double"/>[]), 
    /// содержащий среднемесячные значения температур в порядке обхода элементов словаря.
    /// </returns>
    public static double[] GetMonthAverages(Dictionary<string, int[]> temperature)
    {
        double[] averages = new double[temperature.Count];
        int index = 0;

        foreach (KeyValuePair<string, int[]> pair in temperature)
        {
            double sum = 0;
            foreach (int value in pair.Value)
            {
                sum += value;
            }
            averages[index] = sum / pair.Value.Length;
            index++;
        }

        return averages;
    }
}
