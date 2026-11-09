namespace TumakovTasks;

public static class UtilitesForSongs
{
    public static void PrintPlaylist(List<Song> playlist)
    {
        if (playlist.Count == 0)
        {
            Console.WriteLine("\n-> Плейлист пуст <-");
            return;
        }

        Console.WriteLine("\n-> Список всех песен в плейлисте <-");
        for (int i = 0; i < playlist.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {playlist[i].Title()}");
        }
    }


    public static void CheckBinding(List<Song> playlist)
    {
        Console.WriteLine("\n-> Проверка автоматического связывания между первой и второй песнями <-");
        if (playlist.Count >= 2 && playlist[1].Prev != null)
        {
            Console.WriteLine($"Предыдущая песня для '{playlist[1].Name}' : {playlist[1].Prev.Title()}");
        }
        else
        {
            Console.WriteLine("В списке недостаточно песен для проверки связи");
        }
    }


    public static void FillUserPlaylist(List<Song> playlist)
    {
        Console.WriteLine("\n-> Создание вашего плейлиста <-");
        bool isAddingFinished = false;

        while (!isAddingFinished)
        {
            Console.Write("Введите название песни : ");
            string name = Console.ReadLine()?.Trim()!;

            Console.Write("Введите автора песни : ");
            string author = Console.ReadLine()?.Trim()!;

            try
            {
                playlist.Add(new Song(name, author));
                Console.WriteLine("Песня добавлена");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message} Попробуйте снова.");
                continue;
            }

            Console.Write("Добавить еще одну песню? (yes/no) : ");
            string choice = Console.ReadLine()?.Trim().ToLower()!;
            
            if (choice == "no")
            {
                isAddingFinished = true;
            }
        }
    }


    public static void CompareTwoSongs(List<Song> playlist)
    {
        if (playlist.Count < 2)
        {
            Console.WriteLine("\nДля сравнения необходимо как минимум 2 песни в плейлисте.");
            return;
        }

        Console.WriteLine("\n-> Сравнение песен <-");
        
        int firstIndex = ReadSongIndex("Введите номер первой песни для сравнения : ", playlist.Count);
        int secondIndex = ReadSongIndex("Введите номер второй песни для сравнения : ", playlist.Count);

        Song firstSong = playlist[firstIndex];
        Song secondSong = playlist[secondIndex];

        Console.WriteLine($"\nСравниваем : {firstSong.Title()} И {secondSong.Title()}");

        if (firstSong.Equals(secondSong))
        {
            Console.WriteLine("Результат : Это одинаковые песни (совпадают имя и автор).");
        }
        else
        {
            Console.WriteLine("Результат : Песни разные.");
        }
    }


    private static int ReadSongIndex(string message, int maxCount)
    {
        int index = -1;
        bool isInputValid = false;

        while (!isInputValid)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= maxCount)
            {
                index = choice - 1;
                isInputValid = true;
            }
            else
            {
                Console.WriteLine($"Ошибка : Введите корректное число от 1 до {maxCount}.");
            }
        }

        return index;
    }
}
