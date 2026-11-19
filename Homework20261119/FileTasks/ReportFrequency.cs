namespace FileTasks;

public enum ReportFrequency
{
    Once,            // Разовая задача = один отчет к сроку
    EveryWorkingDay, // Каждый рабочий день (пн-пт)
    Weekly,          // Раз в неделю в выбранный день недели
    Monthly          // Раз в месяц в выбранный день месяца
}
