namespace TumakovTasks;

public class ACipher : BaseCipher, ICipher
{
    private const int DefaultShift = 1;

    public void encode(ref string input) => Shift(ref input, DefaultShift);

    public void decode(ref string input) => Shift(ref input, -DefaultShift);

    private void Shift(ref string input, int step)
    {
        if (string.IsNullOrEmpty(input))
        return;

        int shiftEng = (step % 26 + 26) % 26;
        int shiftRus = (step % 33 + 33) % 33;

        char[] result = new char[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (c < CharToIndex.Length)
            {
                int index = CharToIndex[c];
                if (index != -1)
                {
                    string alphabet = CharToAlphabet[c];
                    int currentShift = alphabet.Length == 26 ? shiftEng : shiftRus;

                    int newIndex = index + currentShift;
                    if (newIndex >= alphabet.Length)
                        newIndex -= alphabet.Length;

                    result[i] = alphabet[newIndex];
                    continue;
                }
            }

            result[i] = c;
        }

        input = new string(result);
    }
}