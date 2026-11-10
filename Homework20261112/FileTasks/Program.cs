namespace FileTasks;

class Program
{
    static void Main(string[] args)
    {
        // Задача в конце файла с дз7
        Console.WriteLine("Задача из конца файла с дз7 - написать программу назначения задач");
        TaskManager taskManager = new TaskManager();

        TopManagement timur = new TopManagement("Тимур", "Генеральный директор", TaskType.Management);
        TopManagement rashid = new TopManagement("Рашид", "Финансовый директор", TaskType.Management, timur);
        TopManagement ilham = new TopManagement("О Ильхам", "Директор по автоматизации", TaskType.Management, timur);

        ITManagement lukas = new ITManagement("Лукас", "Главный бухгалтер", TaskType.Accounting, rashid);

        ITManagement orkadiy = new ITManagement("Оркадий", "Начальник инф систем", TaskType.Management, ilham);
        ITManagement volodya = new ITManagement("Володя", "Зам.начальника", TaskType.Management, orkadiy, orkadiy);

        TeamLead ilshat = new TeamLead("Ильшат", "Главный в секторе системщиков", TaskType.Systems, orkadiy);
        Worker ivanych = new Worker("Иваныч", "Зам. начальника системщиков", TaskType.Systems, ilshat, ilshat);
        Worker vitya = new Worker("Витя", "Системщик", TaskType.Systems, ilshat);
        Worker jeny = new Worker("Женя", "Системщик", TaskType.Systems, ilshat);
        Worker ilia = new Worker("Илья", "Системщик", TaskType.Systems, ilshat);

        TeamLead sergey = new TeamLead("Сергей", "Главный в секторе разработчиков", TaskType.Development, orkadiy);
        Worker laysan = new Worker("Ляйсан", "Зам. начальника разработчиков", TaskType.Development, sergey, sergey);
        Worker marat = new Worker("Марат", "Разработчик", TaskType.Development, sergey);
        Worker dina = new Worker("Дина", "Разработчик", TaskType.Development, sergey);
        Worker ildar = new Worker("Ильдар", "Разработчик", TaskType.Development, sergey);

        Task devTask = new Task("Автоматизировать бухгалтерию", TaskType.Development);
        Task sysTask = new Task("Обновить прошивку микроволновок", TaskType.Systems);

        Console.WriteLine("-> Симуляция назначения задач <-\n");

        taskManager.AssignTask(devTask, volodya, sergey);

        taskManager.AssignTask(sysTask, ivanych, vitya);

        taskManager.AssignTask(sysTask, laysan, vitya);

        taskManager.AssignTask(devTask, lukas, marat);

        taskManager.PrintLogFileToConsole();

        Console.ReadLine();
    }
}
