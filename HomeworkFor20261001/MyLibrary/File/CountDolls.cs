
namespace MyLibrary.File;

public static class CountDolls
{
    public static int GetCountDolls(string[] items)
    {
        int bag = 0;
        foreach (string item in items)
        {
            if (item == "Hello Kitty" || item == "Barbie doll")
                bag++;
        }
        return bag;
    }
}
