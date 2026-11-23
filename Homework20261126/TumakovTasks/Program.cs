namespace TumakovTasks;

class Program
{
    static void Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\nЗадания из методички Тумакова - Лабораторная 10 главы");
            Console.WriteLine("Task 1 - Упражнение 10.1. - ACipher и BCipher");
            Console.WriteLine("Task 2 - Домашнее задание 10.1. - Геометрические объекты");

            Console.Write("\nВыберите номер задания (1-2, 0 для выхода) : ");
            
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 2)
            {
                Console.WriteLine("Ошибка : ожидалось целое число от 0 до 2.");
                continue;
            }

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Завершение работы...");
                    isRunning = false;
                    break;
                case 1: Exercise101(); break;
                case 2: Homework101(); break;
            }
        }
    }
    
    // Упражнение 10.1. - ACipher и BCipher
    internal static void Exercise101()
    {
        Console.WriteLine("Упражнение 10.1. - ACipher и BCipher");

        string text1 = "Привет, World! АБВ";
        ICipher aCipher = new ACipher();

        Console.WriteLine("ACipher (сдвиг на 1)");
        Console.WriteLine("Исходная :      " + text1);
        aCipher.encode(ref text1);
        Console.WriteLine("Зашифрованная : " + text1);
        aCipher.decode(ref text1);
        Console.WriteLine("Расшифрованная : " + text1);

        Console.WriteLine();

        string text2 = "Привет, World! В12ЖЛ";
        ICipher bCipher = new BCipher();

        Console.WriteLine("BCipher (зеркальный)");
        Console.WriteLine("Исходная :      " + text2);
        bCipher.encode(ref text2);
        Console.WriteLine("Зашифрованная : " + text2);
        bCipher.decode(ref text2);
        Console.WriteLine("Расшифрованная : " + text2);
    }

    // Домашнее задание 10.1. - Геометрические объекты
    internal static void Homework101()
    {
        Console.WriteLine("Домашнее задание 10.1. - Геометрические объекты");

        Point point = new Point(10, 20, FigureColor.Black, FigureVisibility.Visible);
        Circle circle = new Circle(0, 0, 5.5, FigureColor.Red, FigureVisibility.Visible);
        Rectangle rectangle = new Rectangle(5, 5, 10, 4, FigureColor.Blue, FigureVisibility.Invisible);

        BaseFigure[] figures = { point, circle, rectangle };

        Console.WriteLine("Исходное состояние фигур");
        foreach (BaseFigure fig in figures)
        {
            fig.PrintInfo();
            Console.WriteLine();
        }

        Console.WriteLine("\nИзменяем фигуры");

        point.MoveHorizontal(15);
        point.MoveVertical(-5);
        point.ChangeColor(FigureColor.Green);

        circle.MoveHorizontal(10);
        circle.ChangeColor(FigureColor.Yellow);

        Console.WriteLine($"Статус видимости прямоугольника : {rectangle.StatusSurvey()}");

        Console.WriteLine("\nСостояние после изменений");
        foreach (BaseFigure fig in figures)
        {
            fig.PrintInfo();
            Console.WriteLine();
        }
    }
}
