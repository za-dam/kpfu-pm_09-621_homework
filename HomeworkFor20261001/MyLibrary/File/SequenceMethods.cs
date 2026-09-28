
namespace MyLibrary.File;

public static class SequenceMethods
{
    public static int[] GenerateSequence(int count)
    {
        int[] numbers = new int[count];
        bool flag = true;
        for (int i = 0; i < count; i++)
        {
            while (flag)
            {
                Console.Write($"Число №{i + 1}: ");
                if (int.TryParse(Console.ReadLine(), out numbers[i]))
                    break;
                Console.WriteLine("Это не целое число, повторите ввод");
            }
        }
        return numbers;
    }

    public static int CheckSequenceIncrease(int[] numbers)
    {
        int violationNumber = -1;
        for (int i = 1; i < numbers.Length; i++)
            if (numbers[i-1] >= numbers[i])
            {
                violationNumber = i;
                break;
            }
        return violationNumber;
    }
}
