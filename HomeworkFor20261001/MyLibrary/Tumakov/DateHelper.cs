
namespace MyLibrary.Tumakov;

public static class DateHelper
{
    static string[] monthNames =
    {
        "января", "февраля", "марта", "апреля", "мая", "июня",
        "июля", "августа", "сентября", "октября", "ноября", "декабря"
    };
    public static bool IsLeapYear(int year)
    {
        return ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0));
    }
    public static int[] GetDaysInMonth(bool isLeap)
    {
        return new int[] {31, isLeap ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    }
    public static string ToDateString(int dayOfYear, int[] daysInMonth)
    {
        int month = 0;
        while (month < 11 && dayOfYear > daysInMonth[month])
        {
            dayOfYear -= daysInMonth[month];
            month++;
        }

        return $"{dayOfYear} {monthNames[month]}";
    }
}
