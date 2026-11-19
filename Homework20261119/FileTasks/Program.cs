using System.Text;

namespace FileTasks;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Задача из конца файла дз8
        // Демонстрация работы распределения задач по проекту и работы с ними

        Person customer = new Person("Иван Петрович", "Заказчик");

        Person teamLead = new Person("Алексей", "Тимлид");
        Person dev1 = new Person("Дмитрий", "Backend-разработчик");
        Person dev2 = new Person("Елена", "Backend-разработчик");
        Person dev3 = new Person("Сергей", "Frontend-разработчик");
        Person dev4 = new Person("Анна", "Frontend-разработчик");
        Person qa1 = new Person("Ольга", "QA-инженер");
        Person qa2 = new Person("Максим", "QA-инженер");
        Person designer = new Person("Мария", "UI/UX Дизайнер");
        Person devOps = new Person("Артем", "DevOps-инженер");
        Person analyst = new Person("Никита", "Системный аналитик");

        List<Person> team = new() { teamLead, dev1, dev2, dev3, dev4, qa1, qa2, designer, devOps, analyst };
        Console.WriteLine($"Команда сформирована. Количество участников: {team.Count}\n");

        // Срок проекта больше года, чтобы в него поместилась задача с ежемесячными отчетами на год
        Project crmProject = new Project(
            "Разработка корпоративной CRM-системы",
            DateTime.Now.AddMonths(13),
            customer,
            teamLead
        );

        // Разовые задачи (один отчет к сроку)
        var task1 = crmProject.CreateTask(teamLead, "Сбор требований и ТЗ", DateTime.Now.AddDays(5), analyst);
        var task2 = crmProject.CreateTask(teamLead, "Дизайн макетов интерфейса", DateTime.Now.AddDays(10), designer);
        var task3 = crmProject.CreateTask(teamLead, "Архитектура базы данных", DateTime.Now.AddDays(12), teamLead);
        var task4 = crmProject.CreateTask(teamLead, "Разработка API авторизации", DateTime.Now.AddDays(15), dev1);
        var task5 = crmProject.CreateTask(teamLead, "Интеграция платежного шлюза", DateTime.Now.AddDays(18), dev2);
        var task6 = crmProject.CreateTask(teamLead, "Верстка личного кабинета", DateTime.Now.AddDays(20), dev3);
        var task7 = crmProject.CreateTask(teamLead, "Настройка графиков аналитики", DateTime.Now.AddDays(22), dev4);
        var task8 = crmProject.CreateTask(teamLead, "Настройка CI/CD пайплайнов", DateTime.Now.AddDays(14), devOps);
        var task9 = crmProject.CreateTask(teamLead, "Написание автотестов API", DateTime.Now.AddDays(25), qa1);
        var task10 = crmProject.CreateTask(teamLead, "Ручное регрессионное тестирование", DateTime.Now.AddDays(28), qa2);
        var task11 = crmProject.CreateTask(teamLead, "Разработка модуля отчетности", DateTime.Now.AddDays(24), dev2);
        var task12 = crmProject.CreateTask(teamLead, "Нагрузочное тестирование API", DateTime.Now.AddDays(26), qa2);

        // Лишние задачи, которые потом будут удалены (в разных статусах)
        var extraTask = crmProject.CreateTask(teamLead, "Интеграция с устаревшим факсом", DateTime.Now.AddDays(30), dev1);
        var obsoleteTask = crmProject.CreateTask(teamLead, "Поддержка Internet Explorer 6", DateTime.Now.AddDays(30), dev3);
        var deprecatedTask = crmProject.CreateTask(teamLead, "Миграция на устаревшую СУБД", DateTime.Now.AddDays(30), dev4);

        // Периодические задачи
        // Отчет каждый рабочий день (две недели - 14 отчетов)
        var dailyTask = crmProject.CreateTask(teamLead, "Ежедневный мониторинг CI/CD пайплайнов",
            DateTime.Now.AddDays(14), devOps, ReportSchedule.EveryWorkingDay);

        // Отчет раз в неделю по пятницам (четыре недели - 4 отчета)
        var weeklyTask = crmProject.CreateTask(teamLead, "Еженедельный статус-отчет для заказчика",
            DateTime.Now.AddDays(28), analyst, ReportSchedule.Weekly(DayOfWeek.Friday));

        // Отчет раз в месяц 5-го числа, задача на год (12 отчетов)
        var monthlyTask = crmProject.CreateTask(teamLead, "Ежемесячный аудит качества и покрытия тестами",
            DateTime.Now.AddYears(1), qa1, ReportSchedule.Monthly(5));


        crmProject.StartExecution();

        // Демонстрация различных сценариев работы с задачами

        // Сценарий 1 : Делегирование задачи в статусе 'Назначена' (dev2 делегирует dev1)
        task5.Delegate(dev2, dev1);
        task5.TakeInWork(dev1);
        task5.SubmitReport(dev1, "Шлюз подключен, тестовые платежи проходят.");
        task5.ApproveReport(teamLead);

        // Сценарий 2 : Отклонение задачи и переназначение (qa2 отклоняет, тимлид передает qa1)
        task10.Reject(qa2);
        task10.Reassign(teamLead, qa1);
        task10.TakeInWork(qa1);
        task10.SubmitReport(qa1, "Чек-лист регресса пройден без критических багов.");
        task10.ApproveReport(teamLead);

        // Сценарий 3 : Удаление задач тимлидом в разных статусах
        // 3а : Отклоненная задача (статус 'Назначена', исполнителя нет)
        extraTask.Reject(dev1);
        crmProject.DeleteTask(teamLead, extraTask);
        // 3б : Задача в статусе 'Назначена' с исполнителем
        crmProject.DeleteTask(teamLead, obsoleteTask);
        // 3в : Задача в статусе 'В работе'
        deprecatedTask.TakeInWork(dev4);
        crmProject.DeleteTask(teamLead, deprecatedTask);

        // Сценарий 4 : Возврат отчета на доработку и повторная сдача
        task2.TakeInWork(designer);
        task2.SubmitReport(designer, "Сделал черновой набросок.");
        task2.ReturnForRevision(teamLead, "Не учтена темная тема, нужно доработать.");
        task2.SubmitReport(designer, "Добавлена темная тема, макеты готовы.");
        task2.ApproveReport(teamLead);

        // Сценарий 5 : Делегирование задачи в статусе 'В работе' (qa1 -> qa2)
        task9.TakeInWork(qa1);
        task9.Delegate(qa1, qa2);
        task9.TakeInWork(qa2);
        task9.SubmitReport(qa2, "Автотесты API написаны, покрытие 85%.");
        task9.ApproveReport(teamLead);

        // Сценарий 6 : Штатное выполнение остальных разовых задач
        // Сначала попытка закрыть проект, пока не всё сделано
        crmProject.TryCloseProject();

        var remainingTasks = new[] { task1, task3, task4, task6, task7, task8, task11, task12 };
        foreach (var task in remainingTasks)
        {
            Person worker = task.Executor!;
            task.TakeInWork(worker);
            task.SubmitReport(worker, $"Задача '{task.Description}' выполнена в полном объеме.");
            task.ApproveReport(teamLead);
        }

        // Сценарий 7 : Периодическая задача, отчет каждый рабочий день
        Console.WriteLine("\nПериодическая задача, отчет каждый рабочий день");
        dailyTask.TakeInWork(devOps);
        CompleteRemainingReports(dailyTask, teamLead);

        // Сценарий 8 : Периодическая задача, отчет раз в неделю (с возвратом первого отчета на доработку)
        Console.WriteLine("\nПериодическая задача, отчет раз в неделю");
        weeklyTask.TakeInWork(analyst);
        weeklyTask.SubmitReport(analyst, "Черновик отчета.", weeklyTask.DueDates[0]);
        weeklyTask.ReturnForRevision(teamLead, "Не указаны риски проекта.");
        CompleteRemainingReports(weeklyTask, teamLead);

        // Сценарий 9 : Периодическая задача, отчет раз в месяц, 12 отчетов
        Console.WriteLine("\n--- Периодическая задача: раз в месяц ---");
        monthlyTask.TakeInWork(qa1);
        monthlyTask.SubmitReport(qa1, "Отчет за первый месяц.", monthlyTask.DueDates[0]);
        monthlyTask.ApproveReport(teamLead);

        CompleteRemainingReports(monthlyTask, teamLead);

        
        crmProject.TryCloseProject();

        // Итоговая нагрузка : у каждого из 10 человек должна быть минимум одна задача
        Console.WriteLine("\n--> Задачи по сотрудникам <--");
        foreach (var person in team)
        {
            int count = crmProject.Tasks.Count(t => t.Executor == person);
            Console.WriteLine($"{person.Name} ({person.Role}) : задач {count}");
        }

        bool everyoneHasTask = team.All(p => crmProject.Tasks.Any(t => t.Executor == p));
        Console.WriteLine($"\nУ каждого сотрудника есть минимум одна задача : {(everyoneHasTask ? "да" : "НЕТ")}");
    }

    // Сдает и утверждает все оставшиеся отчеты периодической задачи по расписанию
    // Каждый отчет сдается в свою плановую дату (так можно показать год работы за один запуск)
    private static void CompleteRemainingReports(ProjectTask task, Person lead)
    {
        Person worker = task.Executor!;
        while (task.Status == ProjectTaskStatus.InProgress)
        {
            DateTime due = task.DueDates[task.ReportsApproved];
            task.SubmitReport(worker, $"Отчет за {due:dd.MM.yyyy}: работа выполнена.", due);
            task.ApproveReport(lead);
        }
    }
}
