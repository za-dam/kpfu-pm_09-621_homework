namespace FileTasks;

public class TaskManager
{
    private readonly string _logFilePath = "Logs/company_audit.log";

    public void AssignTask(Task task, Employee sender, Employee receiver)
    {
        Console.WriteLine($"От человека {sender.Name} ({sender.Title}) даётся задача <{task.Title}> человеку {receiver.Name} ({receiver.Title})");

        TaskStatus finalStatus;
        string comment;

        bool isDepartmentValid = (receiver.Department == task.Type) || 
                                    (task.Type == TaskType.Management && (receiver is ITManagement || receiver is TopManagement));

        if (!isDepartmentValid)
        {
            finalStatus = TaskStatus.Rejected;
            comment = $"Ошибка отдела : задача типа {task.Type} назначена в отдел {receiver.Department}.";
                
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"-> {receiver.Name} отклоняет задачу (Автоотказ : {comment}).\n");
            Console.ResetColor();
            WriteLogToFile(task, sender, receiver, finalStatus, comment);
            return;
        }

        if (receiver.CanAcceptTaskFrom(sender))
        {
            finalStatus = TaskStatus.Accepted;
            comment = "Задача успешно назначена";
                
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"-> {receiver.Name} берёт задачу.\n");
        }
        else
        {
            finalStatus = TaskStatus.Rejected;
            comment = "Ошибка : отправитель не является прямым руководителем или его заместителем";
                
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"-> {receiver.Name} отклоняет задачу (Автоотказ : {comment}).\n");
        }
        Console.ResetColor();

        WriteLogToFile(task, sender, receiver, finalStatus, comment);
    }


    private void WriteLogToFile(Task task, Employee sender, Employee receiver, TaskStatus status, string comment)
    {
        try
        {
            string directoryPath = Path.GetDirectoryName(_logFilePath);

            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string statusText = status == TaskStatus.Accepted ? "ALLOWED" : "REJECTED";
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string logLine = $"[{timestamp}] [{statusText}] " +
                                $"From: {sender.Name} ({sender.Title}) | " +
                                $"To: {receiver.Name} ({receiver.Title}) | " +
                                $"Task: \"{task.Title}\" (Type: {task.Type}) | " +
                                $"Details: {comment}{Environment.NewLine}";

            File.AppendAllText(_logFilePath, logLine);
            }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось записать лог в файл : {ex.Message}");
        }
    }

    public void PrintLogFileToConsole()
    {
        Console.WriteLine($"\n-> Содержимое файла логов по пути - ({_logFilePath}) <-");
        if (File.Exists(_logFilePath))
        {
            string[] logs = File.ReadAllLines(_logFilePath);
            foreach (var line in logs)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("Файл логов еще не создан.");
        }
    }
}