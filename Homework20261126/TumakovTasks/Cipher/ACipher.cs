namespace TumakovTasks;

public class ACipher : BaseCipher, ICipher
{
    public void encode(ref string input) => Shift(ref input, 1);

    public void decode(ref string input) => Shift(ref input, -1);

    private void Shift(ref string input, int step)
    {
        if (string.IsNullOrEmpty(input))
            return;

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
                    int newIndex = index + step;

                    if (newIndex >= alphabet.Length)
                        newIndex = 0;
                    else if (newIndex < 0)
                        newIndex = alphabet.Length - 1;

                    result[i] = alphabet[newIndex];
                    continue;
                }
            }

            result[i] = c;
        }

        input = new string(result);
    }
}