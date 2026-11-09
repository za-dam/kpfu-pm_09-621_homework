using TumakovTasks;

public class SearchMailSuperAdvancedTests
{
    [Theory]
    [InlineData("Иванов # iviviv@mail.ru,телефон", "iviviv@mail.ru")]
    [InlineData("Петров # petr.work@gmail.com;Москва", "petr.work@gmail.com")]
    [InlineData("Антонов # |admin_main@test.com|", "admin_main@test.com")]
    [InlineData("Сергеев # (serg-99@yandex.ru)", "serg-99@yandex.ru")]
    [InlineData("Смирнов # ivan.ivanov_2026+test@mail.ru", "ivan.ivanov_2026+test@mail.ru")]
    public void SearchMail_WithPunctuationNoise_ExtractsCleanEmail(string s, string expected)
    {
        string testString = s;

        Utilites.SearchMail(ref testString);

        Assert.Equal(expected, testString);
    }
}
