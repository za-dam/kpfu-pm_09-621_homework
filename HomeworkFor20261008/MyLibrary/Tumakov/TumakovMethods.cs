namespace MyLibrary.Tumakov;

public static class TumakovMethods
{
    /// <summary>
    /// Сравнивает два целых числа и возвращает наибольшее из них.
    /// </summary>
    /// <param name="firstNumber">Ссылка на первое сравниваемое число.</param>
    /// <param name="secondNumber">Ссылка на второе сравниваемое число.</param>
    /// <returns>Наибольшее из двух переданных чисел.</returns>
    public static int LargerNumber(ref int firstNumber, ref int secondNumber)
    {
        return firstNumber > secondNumber ? firstNumber : secondNumber;
    }

    /// <summary>
    /// Меняет местами значения двух целочисленных переменных.
    /// </summary>
    /// <param name="firstParam">Ссылка на первую переменную, значение которой будет заменено на значение <paramref name="secondParam"/>.</param>
    /// <param name="secondParam">Ссылка на вторую переменную, значение которой будет заменено на значение <paramref name="firstParam"/>.</param>
    public static void SwapParams(ref int firstParam, ref int secondParam)
    {
        int temp = firstParam;
        firstParam = secondParam;
        secondParam = temp;
    }

    /// <summary>
    /// Пытается вычислить факториал заданного числа с защитой от переполнения.
    /// </summary>
    /// <param name="number">Ссылка на число, факториал которого необходимо вычислить.</param>
    /// <param name="result">Когда метод завершает работу, содержит вычисленный факториал, если вычисление прошло успешно; в противном случае — <c>0</c>.</param>
    /// <returns><see langword="true"/>, если факториал успешно вычислен без переполнения; в противном случае — <see langword="false"/> (например, если результат превысил максимальное значение <see cref="ulong"/>).</returns>
    public static bool TryCalculateFactorial(ref ulong number, out ulong result)
    {
        if (number == 0 || number == 1)
        {
            result = 1;
            return true;
        }

        result = 1;

        try
        {
            checked
            {
                for (ulong i = 1 ; i <= number; i++)
                {
                    result*=i;
                }
            }
            return true;
        }
        catch (OverflowException)
        {
            result = 0;
            return false;
        }
    }

    /// <summary>
    /// Рекурсивно вычисляет факториал заданного числа.
    /// </summary>
    /// <param name="number">Число, факториал которого необходимо вычислить.</param>
    /// <returns>Факториал переданного числа.</returns>
    private static ulong RecursionFactorial(ulong number)
    {
        if (number == 0 || number == 1)
        {
            return 1;
        }
        
        return number * RecursionFactorial(number - 1);
    }

    /// <summary>
    /// Пытается рекурсивно вычислить факториал числа с контролем арифметического переполнения.
    /// </summary>
    /// <param name="number">Число типа <see cref="ulong"/>, для которого необходимо вычислить факториал.</param>
    /// <param name="result">Выходной параметр. В случае успеха содержит вычисленный факториал; в случае переполнения равен 0.</param>
    /// <returns>
    /// Возвращает <see langword="true"/>, если факториал успешно вычислен и поместился в диапазон <see cref="ulong"/> (для чисел от 0 до 20); 
    /// иначе — <see langword="false"/> (для чисел 21 и более).
    /// </returns>
    public static bool TryRecursionFactorial(ulong number, out ulong result)
    {
        try
        {
            checked
            {
                result = RecursionFactorial(number);
                return true;
            }
        }
        catch (OverflowException)
        {
            result = 0;
            return false;
        }
    }

    /// <summary>
    /// Вычисляет наибольший общий делитель (НОД) двух целых неотрицательных чисел с помощью алгоритма Евклида.
    /// </summary>
    /// <param name="firstNumber">Первое число для вычисления НОД.</param>
    /// <param name="secondNumber">Второе число для вычисления НОД.</param>
    /// <returns>Наибольший общий делитель двух переданных чисел.</returns>
    public static uint GetGCD(uint firstNumber, uint secondNumber)
    {
        while (secondNumber != 0)
        {
            uint temp = secondNumber;
            secondNumber = firstNumber % secondNumber;
            firstNumber = temp;
        }
        return firstNumber;
    }

    /// <summary>
    /// Вычисляет наибольший общий делитель (НОД) трех целых неотрицательных чисел.
    /// </summary>
    /// <param name="firstNumber">Первое число для вычисления НОД.</param>
    /// <param name="secondNumber">Второе число для вычисления НОД.</param>
    /// <param name="thirdNumber">Третье число для вычисления НОД.</param>
    /// <returns>Наибольший общий делитель трех переданных чисел.</returns>
    public static uint GetGCD(uint firstNumber, uint secondNumber, uint thirdNumber)
    {
        return GetGCD(GetGCD(firstNumber, secondNumber), thirdNumber);
    }

    /// <summary>
    /// Рекурсивно вычисляет N-е число последовательности Фибоначчи.
    /// </summary>
    /// <param name="number">Порядковый номер числа Фибоначчи (индекс элемента в последовательности).</param>
    /// <returns>Значение N-го числа Фибоначчи.</returns>
    private static ulong RecursionFibonacci(ulong number)
    {
        if (number == 1) 
        {
            return number;
        }
        return RecursionFibonacci(number - 2) + RecursionFibonacci(number - 1);
    }

    /// <summary>
    /// Пытается рекурсивно вычислить n-е число ряда Фибоначчи с контролем арифметического переполнения.
    /// </summary>
    /// <param name="number">Порядковый номер числа в ряду Фибоначчи (индексация начинается с 1).</param>
    /// <param name="result">Выходной параметр. В случае успеха содержит n-е число Фибоначчи; в случае переполнения равен 0.</param>
    /// <returns>
    /// Возвращает <see langword="true"/>, если число успешно вычислено и поместилось в диапазон <see cref="ulong"/> (для n от 1 до 93); 
    /// иначе — <see langword="false"/> (при арифметическом переполнении для n >= 94).
    /// </returns>
    public static bool TryRecursionFibonacci(ulong number, out ulong result)
    {
        try
        {
            checked
            {
                result = RecursionFibonacci(number);
                return true;
            }
        }
        catch (OverflowException)
        {
            result = 0;
            return false;
        }
    }
}
