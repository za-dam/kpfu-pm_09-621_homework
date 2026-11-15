namespace FileTasks;

class Program
{
    static void Main(string[] args)
    {
        // Задача из конца файла дз8
        // демонстрация работы распределения задач по проекту и раблоту с ними

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

        Project crmProject = new Project(
            "Разработка корпоративной CRM-системы",
            DateTime.Now.AddMonths(2),
            customer,
            teamLead
        );

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

        var extraTask = crmProject.CreateTask(teamLead, "Интеграция с устаревшим факсом", DateTime.Now.AddDays(30), dev1);

        crmProject.StartExecution();

        //Демонстрация различных сценариев работы с задачами

        // Сценарий 1 : Делегирование задачи (dev2 делегирует dev1)
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

        // Сценарий 3 : Отклонение лишней задачи и удаление её тимлидом
        extraTask.Reject(dev1);
        crmProject.DeleteRejectedTask(teamLead, extraTask);

        // Сценарий 4 : Возврат отчета на доработку и повторная сдача
        task2.TakeInWork(designer);
        task2.SubmitReport(designer, "Сделал черновой набросок.");
        task2.ReturnForRevision(teamLead, "Не учтена темная тема, нужно доработать.");
        task2.SubmitReport(designer, "Добавлена темная тема, макеты готовы.");
        task2.ApproveReport(teamLead);

        // Сценарий 5 : Штатное выполнение остальных задач
        var remainingTasks = new[] { task1, task3, task4, task6, task7, task8, task9 };
        foreach (var task in remainingTasks)
        {
            Person worker = task.Executor!;
            task.TakeInWork(worker);
            task.SubmitReport(worker, $"Задача '{task.Description}' выполнена в полном объеме.");
            task.ApproveReport(teamLead);
        }

        crmProject.TryCloseProject();
    }
}
