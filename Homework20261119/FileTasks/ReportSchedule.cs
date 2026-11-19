namespace FileTasks;

/// <summary>
/// Правило отчетности по задаче. Сами даты считаются при создании задачи
/// по сроку задачи: GetDueDates(начало, конец).
/// </summary>
public sealed class ReportSchedule
{
    private readonly DayOfWeek _weekDay;
    private readonly int _dayOfMonth;

    public ReportFrequency Frequency { get; }

    private ReportSchedule(ReportFrequency frequency, DayOfWeek weekDay = DayOfWeek.Monday, int dayOfMonth = 1)
    {
        Frequency = frequency;
        _weekDay = weekDay;
        _dayOfMonth = dayOfMonth;
    }

    public static ReportSchedule Once { get; } = new(ReportFrequency.Once);

    public static ReportSchedule EveryWorkingDay { get; } = new(ReportFrequency.EveryWorkingDay);

    public static ReportSchedule Weekly(DayOfWeek weekDay)
    {
        if (!Enum.IsDefined(typeof(DayOfWeek), weekDay))
            throw new ArgumentOutOfRangeException(nameof(weekDay), "Некорректный день недели.");
        return new(ReportFrequency.Weekly, weekDay: weekDay);
    }

    public static ReportSchedule Monthly(int dayOfMonth)
    {
        if (dayOfMonth < 1 || dayOfMonth > 31)
            throw new ArgumentOutOfRangeException(nameof(dayOfMonth), "День месяца должен быть от 1 до 31.");
        return new(ReportFrequency.Monthly, dayOfMonth: dayOfMonth);
    }

    /// <summary>
    /// Даты сдачи отчетов в промежутке (start; end], то есть начиная со дня после создания задачи
    /// и до срока задачи включительно. Для разовой задачи - одна дата, срок задачи.
    /// Праздники не учитываются: рабочий день - это любой день с понедельника по пятницу.
    /// Если в месяце меньше дней, чем выбранный день (например, 31), отчет сдается в последний день месяца.
    /// </summary>
    public IReadOnlyList<DateTime> GetDueDates(DateTime start, DateTime end)
    {
        DateTime from = start.Date;
        DateTime to = end.Date;
        var result = new List<DateTime>();

        switch (Frequency)
        {
            case ReportFrequency.Once:
                result.Add(to);
                break;

            case ReportFrequency.EveryWorkingDay:
                for (DateTime d = from.AddDays(1); d <= to; d = d.AddDays(1))
                {
                    if (d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday)
                        result.Add(d);
                }
                break;

            case ReportFrequency.Weekly:
                for (DateTime d = from.AddDays(1); d <= to; d = d.AddDays(1))
                {
                    if (d.DayOfWeek == _weekDay)
                        result.Add(d);
                }
                break;

            case ReportFrequency.Monthly:
                for (DateTime month = new DateTime(from.Year, from.Month, 1); month <= to; month = month.AddMonths(1))
                {
                    int day = Math.Min(_dayOfMonth, DateTime.DaysInMonth(month.Year, month.Month));
                    DateTime due = new DateTime(month.Year, month.Month, day);
                    if (due > from && due <= to)
                        result.Add(due);
                }
                break;
        }

        return result.AsReadOnly();
    }

    public override string ToString()
    {
        string[] weekDays = { "воскресеньям", "понедельникам", "вторникам", "средам", "четвергам", "пятницам", "субботам" };

        return Frequency switch
        {
            ReportFrequency.Once => "однократно",
            ReportFrequency.EveryWorkingDay => "каждый рабочий день",
            ReportFrequency.Weekly => $"раз в неделю (по {weekDays[(int)_weekDay]})",
            ReportFrequency.Monthly => $"раз в месяц ({_dayOfMonth}-го числа)",
            _ => Frequency.ToString()
        };
    }
}
