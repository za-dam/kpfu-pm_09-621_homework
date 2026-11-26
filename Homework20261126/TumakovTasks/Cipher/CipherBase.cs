namespace TumakovTasks;

public abstract class CipherBase
{
    private const string EngLower = "abcdefghijklmnopqrstuvwxyz";
    private const string RusLower = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    private const string EngUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string RusUpper = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

    protected static readonly int[] CharToIndex = new int[1106];
    protected static readonly string[] CharToAlphabet = new string[1106];

    static CipherBase()
    {
        Array.Fill(CharToIndex, -1);
        RegisterAlphabet(EngLower);
        RegisterAlphabet(EngUpper);
        RegisterAlphabet(RusLower);
        RegisterAlphabet(RusUpper);
    }

    private static void RegisterAlphabet(string alphabet)
    {
        for (int i = 0; i < alphabet.Length; i++)
        {
            char c = alphabet[i];
            CharToIndex[c] = i;
            CharToAlphabet[c] = alphabet;
        }
    }
}
