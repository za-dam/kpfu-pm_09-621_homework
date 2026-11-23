namespace TumakovTasks.Tests;

public class CipherTests
{
    private readonly ICipher _aCipher = new ACipher();
    private readonly ICipher _bCipher = new BCipher();

    [Theory]
    [InlineData("АБВ", "БВГ")]
    [InlineData("абв", "бвг")]
    [InlineData("ABC", "BCD")]
    [InlineData("abc", "bcd")]
    [InlineData("еёж", "ёжз")]
    public void ACipher_Encode_ShiftsLettersByOne(string input, string expected)
    {
        string text = input;
        _aCipher.encode(ref text);
        Assert.Equal(expected, text);
    }

    [Theory]
    [InlineData("я", "а")]
    [InlineData("Я", "А")]
    [InlineData("z", "a")]
    [InlineData("Z", "A")]
    public void ACipher_Encode_WrapsAroundAlphabetEnd(string input, string expected)
    {
        string text = input;
        _aCipher.encode(ref text);
        Assert.Equal(expected, text);
    }

    [Theory]
    [InlineData("БВГ", "АБВ")]
    [InlineData("а", "я")]
    [InlineData("A", "Z")]
    public void ACipher_Decode_RevertsEncode(string input, string expected)
    {
        string text = input;
        _aCipher.decode(ref text);
        Assert.Equal(expected, text);
    }

    [Theory]
    [InlineData("В", "Э")]
    [InlineData("в", "э")]
    [InlineData("АЯ", "ЯА")]
    [InlineData("AZ", "ZA")]
    [InlineData("az", "za")]
    [InlineData("п", "п")]
    public void BCipher_Encode_MirrorsLetters(string input, string expected)
    {
        string text = input;
        _bCipher.encode(ref text);
        Assert.Equal(expected, text);
    }

    [Fact]
    public void BCipher_Decode_RestoresOriginalText()
    {
        string original = "Привет, World! 123";
        string text = original;

        _bCipher.encode(ref text);
        Assert.NotEqual(original, text);

    _bCipher.decode(ref text);
    Assert.Equal(original, text);
    }

    [Theory]
    [InlineData("12345 !?.,-@#$%^&*()")]
    [InlineData("")]
    [InlineData(null)]
    public void Ciphers_IgnoreNonAlphabetAndEmptyStrings(string? input)
    {
        string textA = input!;
        string textB = input!;

        _aCipher.encode(ref textA);
        _bCipher.encode(ref textB);

        Assert.Equal(input, textA);
        Assert.Equal(input, textB);
    }

    [Fact]
    public void Ciphers_FullCycle_MixedLanguagesAndCases()
    {
        string original = "Съешь ещё этих мягких французских булок, да выпей чаю! The quick brown fox.";
            
        string textA = original;
        _aCipher.encode(ref textA);
        _aCipher.decode(ref textA);
        Assert.Equal(original, textA);

        string textB = original;
        _bCipher.encode(ref textB);
        _bCipher.decode(ref textB);
        Assert.Equal(original, textB);
    }
}
