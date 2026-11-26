namespace TumakovTasks;

public class BCipher : CipherBase, ICipher
{
    public void encode(ref string input)
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
                    int mirrorIndex = alphabet.Length - index - 1;

                    result[i] = alphabet[mirrorIndex];
                    continue;
                }
            }

            result[i] = c;
        }

        input = new string(result);
    }

    public void decode(ref string input)
    {
        encode(ref input);
    }
}
