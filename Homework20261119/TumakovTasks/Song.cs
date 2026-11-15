namespace TumakovTasks;

public class Song
{

    private string _name;
    private string _author;
    private Song _prev;

    public Song()
    {
        _name = "Неизвестно";
        _author = "Неизвестен";
        _prev = null;
    }

    public Song(string name, string author) : this(name, author, null)
    {
    }

    public Song(string name, string author, Song prev)
    {
        Name = name;
        Author = author;
        Prev = prev;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Название песни не может быть пустой строкой.");
            }
            _name = value.Trim();
        }
    }

    public string Author
    {
        get => _author;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Автор песни не может быть пустой строкой.");
            }
            _author = value.Trim();
        }
    }

    public Song Prev
    {
        get => _prev;
        set
        {
            if (value == this)
            {
                throw new InvalidOperationException("Песня не может ссылаться на саму себя.");
            }
            _prev = value;
        }
    }

    public void Print()
    {
        Console.WriteLine(Title());
    }

    public string Title()
    {
        return $"\"{_name}\" -><- {_author}";
    }

    public override bool Equals(object d)
    {
        if (d == null) return false;

        Song otherSong = d as Song;
        if (otherSong == null) return false;

        return _name == otherSong._name && _author == otherSong._author;
    }

    // Не используется в текущей реализации через списки, возможно использование, к примеру, с HashSet
    public override int GetHashCode()
    {
        return HashCode.Combine(_name, _author);
    }
}
