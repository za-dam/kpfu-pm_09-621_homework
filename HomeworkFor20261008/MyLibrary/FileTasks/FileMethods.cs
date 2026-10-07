using System;

namespace MyLibrary.FileTasks;

public class FileMethods
{
    /// <summary>
    /// Вычисляет сумму, среднее арифметическое и произведение переданного набора целых чисел.
    /// </summary>
    /// <param name="average">Когда метод завершает работу, содержит среднее арифметическое всех элементов; если массив пуст или равен <see langword="null"/>, возвращает <c>0</c>.</param>
    /// <param name="product">Ссылка на переменную, которая перезаписывается произведением всех элементов массива; если массив пуст или равен <see langword="null"/>, возвращает <c>0</c>.</param>
    /// <param name="numbers">Последовательность или массив целых чисел для обработки.</param>
    /// <returns>Сумму всех переданных чисел; если массив пуст или равен <see langword="null"/>, возвращает <c>0</c>.</returns>
    /// <remarks>
    /// Метод устойчив к передаче <see langword="null"/> или пустого набора параметров и не генерирует исключений, сбрасывая все выходные значения в ноль.
    /// </remarks>
    public static int ProcessNumbers(out double average, ref double product, params int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
        {
            product = 0;
            average = 0;
            return 0;
        }

        int sum = 0;
        product = 1;

        foreach (int num in numbers)
        {
            sum += num;
            product *= num;
        }

        average = (double)sum / numbers.Length;

        return sum;    
    }

    /// <summary>
    /// Генерирует массив случайных целых чисел в диапазоне от 1 до 100 включительно.
    /// </summary>
    /// <param name="lenght">Размер (длина) создаваемого массива.</param>
    /// <returns>Массив, заполненный случайными числами.</returns>
    public static int[] GenerateRandomArray(int lenght)
    {
        int[] arrayNumbers = new int[lenght];
        Random random = new Random();

        for (int i = 0; i < arrayNumbers.Length; i++)
        {
            arrayNumbers[i] = random.Next(1, 101);
        }

        return arrayNumbers;
    }

    /// <summary>
    /// Выводит в консоль псевдографическое (ASCII-арт) представление заданной цифры.
    /// </summary>
    /// <param name="digit">Ссылка на переменную, содержащую цифру для отрисовки.</param>
    /// <remarks>
    /// Метод корректно обрабатывает только одиночные цифры в диапазоне от <c>0</c> до <c>9</c> включительно. 
    /// При передаче чисел вне этого диапазона отрисовка псевдографики произведена не будет (выведется только заголовок).
    /// </remarks>
    public static void DrawDigit(ref int digit)
    {
        Console.WriteLine($"\nРисуем цифру {digit}:");
        switch (digit)
        {
            case 0:
                Console.WriteLine(" ### \n#   #\n#   #\n#   #\n ### ");
                break;
            case 1:
                Console.WriteLine("  #  \n ##  \n  #  \n  #  \n ### ");
                break;
            case 2:
                Console.WriteLine(" ### \n#   #\n  ## \n #   \n#####");
                break;
            case 3:
                Console.WriteLine("#### \n    #\n ### \n    #\n#### ");
                break;
            case 4:
                Console.WriteLine("#   #\n#   #\n#####\n    #\n    #");
                break;
            case 5:
                Console.WriteLine("#####\n#    \n#### \n    #\n#### ");
                break;
            case 6:
                Console.WriteLine(" ### \n#    \n#### \n#   #\n ### ");
                break;
            case 7:
                Console.WriteLine("#####\n    #\n   # \n  #  \n #   ");
                break;
            case 8:
                Console.WriteLine(" ### \n#   #\n ### \n#   #\n ### ");
                break;
            case 9:
                Console.WriteLine(" ### \n#   #\n ####\n    #\n ### ");
                break;
        }
        Console.WriteLine();
    }
}
