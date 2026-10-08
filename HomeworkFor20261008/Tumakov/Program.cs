using MyLibrary.Tumakov;


namespace Tumakov;

class Program
{
    static void Main(string[] args)
    {
        //Задания из методички Тумакова - Лабораторная 5 главы
        Console.WriteLine("Задания из методички Тумакова - Лабораторная 5 главы");

        //Выводим все возможные на выбор задания с кратким пояснением
        Console.WriteLine("(Task 1) Упражнение 5.1. - метод, возвращающий наибольшее из двух целых чисел");
        Console.WriteLine("(Task 2) Упражнение 5.2. - метод, меняющий местами два значения двух передаваемых по ссылке параметров");
        Console.WriteLine("(Task 3) Упражнение 5.3. - метод, высчитывающий факториал, отслеживающий переполнение значения");
        Console.WriteLine("(Task 4) Упражнение 5.4. - рекурсивный метод вычисления факториала");
        Console.WriteLine("(Task 5) Домашнее задание 5.1. - метод вычисления НОД для 2 и 3 натуральных чисел");
        Console.WriteLine("(Task 6) Домашнее задание 5.2. - рекурсивный метод вычисления n-ого числа ряда Фибоначчи");

        // Запускаем бесконечный цикл, чтобы пользователь мог тестировать разные задачи без перезапуска программы
        while (true)
        {
            Console.Write("Выберите номер необходимого задания(1-6) (0 для завершения работы) : ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice < 7)
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
                    case 5: Task5(); break;
                    case 6: Task6(); break;
                }
            }    
            else
            {
                Console.WriteLine("\nОшибка : Некорректный ввод. Ожидалось целое число от 0 до 6.");
            }         
        }
    }

    //Упражнение 5.1. - метод, возвращающий наибольшее из двух целых чисел
    internal static void Task1()
    {
        Console.WriteLine("(Task 1) Упражнение 5.1. - метод, возвращающий наибольшее из двух целых чисел");

        Console.WriteLine("Введите два целых числа");

        Console.Write("Введите первое число : ");
        bool isFirstValid = int.TryParse(Console.ReadLine(), out int numberOne);
        
        Console.Write("Введите второе число : ");
        bool isSecondValid = int.TryParse(Console.ReadLine(), out int numberTwo);

        if (isFirstValid && isSecondValid)
        {
            Console.WriteLine($"\nНаибольшее из двух чисел - {TumakovMethods.LargerNumber(ref numberOne, ref numberTwo)}");
        }
        else
        {
            Console.WriteLine("\nОшибка : Некорректный ввод. Ожидаются целые числа.");
            return;   
        }
    }

    //Упражнение 5.2. - метод, меняющий местами два значения двух передаваемых по ссылке параметров
    internal static void Task2()
    {
        Console.WriteLine("(Task 2) Упражнение 5.2. - метод, меняющий местами два значения двух передаваемых по ссылке параметров");

        Console.WriteLine("Введите два параметра");

        Console.Write("Первый параметр : ");
        bool isFirstValid = int.TryParse(Console.ReadLine(), out int firstParam);

        Console.Write("Второй параметр : ");
        bool isSecondValid = int.TryParse(Console.ReadLine(), out int secondParam);

        if (isFirstValid && isSecondValid)
        {
            Console.WriteLine($"\nДо замены : первый параметр - {firstParam}, второй параметр - {secondParam}");

            TumakovMethods.SwapParams(ref firstParam, ref secondParam);

            Console.WriteLine($"После замены : первый параметр - {firstParam}, второй параметр - {secondParam}");
        }
        else
        {
            Console.WriteLine("\nОшибка : Некорректный ввод. Ожидаются целые числа.");
            return; 
        }
    }

    //Упражнение 5.3. - метод, высчитывающий факториал, отслеживающий переполнение значения
    internal static void Task3()
    {
        Console.WriteLine("(Task 3) Упражнение 5.3. - метод, высчитывающий факториал, отслеживающий переполнение значения");

        Console.Write("Введите целое неотрицательное число для расчёта его факториала : ");

        if (ulong.TryParse(Console.ReadLine(), out ulong userNumber) && userNumber >= 0)
        {
            if (TumakovMethods.TryCalculateFactorial(ref userNumber, out ulong factorialNumber))
            {
                Console.WriteLine($"\nФакториал {userNumber} равен: {factorialNumber}");
            }
            else
            {
                Console.WriteLine($"\nОшибка : При вычислении {userNumber}! произошло переполнение.");
                return;
            }
        }
        else
        {
            Console.WriteLine("\nОшибка : Некорректный ввод. Ожидается целое неотрицательное число.");
            return;   
        }
    }

    //Упражнение 5.4. - рекурсивный метод вычисления факториала
    internal static void Task4()
    {
        Console.WriteLine("(Task 4) Упражнение 5.4. - рекурсивный метод вычисления факториала");

        Console.Write("Введите целое неотрицательное число от 0 до 20 для расчёта его факториала рекурсивно : ");

        if (ulong.TryParse(Console.ReadLine(), out ulong userNumber) && userNumber >= 0)
        {
            if (TumakovMethods.TryRecursionFactorial(userNumber, out ulong factorialNumber))
            {
                Console.WriteLine($"\nФакториал {userNumber} равен: {factorialNumber}");
            }
            else
            {
                Console.WriteLine($"\nОшибка : При вычислении {userNumber}! произошло переполнение.");
                return;
            }
        }
        else
        {
            Console.WriteLine("\nОшибка : Некорректный ввод. Ожидается целое неотрицательное число.");
            return;
        }
    }

    //Домашнее задание 5.1. - метод вычисления НОД для 2 и 3 натуральных чисел
    internal static void Task5()
    {
        Console.WriteLine("(Task 5) Домашнее задание 5.1. - метод вычисления НОД для 2 и 3 натуральных чисел");

        Console.WriteLine("Введите три натуральных числа");

        Console.Write("Введите первое натуральное число : ");
        bool isFirstValid = uint.TryParse(Console.ReadLine(), out uint firstNumber) && firstNumber > 0;

        Console.Write("Введите второе натуральное число : ");
        bool isSecondValid = uint.TryParse(Console.ReadLine(), out uint secondNumber) && secondNumber > 0;

        Console.Write("Введите третье натуральное число : ");
        bool isThirdValid = uint.TryParse(Console.ReadLine(), out uint thirdNumber) && thirdNumber > 0;

        if (isFirstValid && isSecondValid && isThirdValid)
        {
            uint gcdOfAll = TumakovMethods.GetGCD(firstNumber, secondNumber, thirdNumber);
            uint gcdFirstSecond = TumakovMethods.GetGCD(firstNumber, secondNumber);
            Console.WriteLine($"\nНаибольший общий делитель чисел ({firstNumber}, {secondNumber}, {thirdNumber}) равен : {gcdOfAll}");
            Console.WriteLine($"\nНаибольший общий делитель чисел ({firstNumber}, {secondNumber}) равен : {gcdFirstSecond}");
        }
        else
        {
            Console.WriteLine("\nОшибка: Некорректный ввод. Ожидаются натуральные числа.");
            return;
        }
    }

    //Домашнее задание 5.2. - рекурсивный метод вычисления n-ого числа ряда Фибоначчи
    internal static void Task6()
    {
        Console.WriteLine("(Task 6) Домашнее задание 5.2. - рекурсивный метод вычисления n-ого числа ряда Фибоначчи");

        Console.Write("Введите номер n-ого числа ряда Фибоначчи (от 1 до 93): ");

        if (ulong.TryParse(Console.ReadLine(), out ulong userNumber) && userNumber <= 93 && userNumber > 0)
        {
            if (TumakovMethods.TryRecursionFibonacci(userNumber, out ulong fibonacciResult))
            {
                Console.WriteLine($"Значение {userNumber}-ого числа ряда Фибоначчи равно {fibonacciResult}");
            }
            else
            {
                Console.WriteLine($"\nОшибка : При вычислении {userNumber}-ого числа ряда Фибоначчи произошло переполнение.");
                return;
            }
        }
        else
        {
            Console.WriteLine("\nОшибка: Некорректный ввод. Ожидается натуральное число от 1 до 93.");
            return;
        }
    }
}
