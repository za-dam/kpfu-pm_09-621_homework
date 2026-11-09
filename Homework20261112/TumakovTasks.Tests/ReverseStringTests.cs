using TumakovTasks;

public class ReverseStringTests
{
    [Theory]

    [InlineData("hello", "olleh")]
    [InlineData("12345", "54321")]
    
    [InlineData("", "")]
    [InlineData(null, null)]
    
    [InlineData("a", "a")]
    [InlineData("radar", "radar")]
    public void ReverseString_WhenCalled_ReturnsExpectedResult(string input, string expected)
    {
        string result = Utilites.ReverseString(input);

        Assert.Equal(expected, result);
    }
}
