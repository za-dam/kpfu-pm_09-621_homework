
namespace MyLibrary.File;

public static class CardValue
{
    public static string GetCardValue(int number)
    {
        return number switch
        {
            6 => "шестёрка",
                7 => "семёрка",
                8 => "восьмёрка",
                9 => "девятка",
                10 => "десятка",
                11 => "валет",
                12 => "дама",
                13 => "король",
                _ => "туз"
        };
    }
}
