namespace FileTasks;


public class Player
{
    private DateTime _birthDate;
    private readonly Guid _id;

    public string Name { get; }

    public int Age => CalculateAge(_birthDate);

    public Guid Id => _id; 

    public DateTime BirthDate
    {
        get => _birthDate;
        set
        {
            int age = CalculateAge(value);

            if (age < 1 || age > 120)
            {
                throw new ArgumentException("Возраст должен быть от 1 до 120 лет.");
            }

            _birthDate = value;
        }
    }

    private int CalculateAge(DateTime birthDate)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    public decimal Balance { get; private set; }
    public int TicketsBought { get; private set; }
    public decimal TotalWinnings { get; private set; }

    public bool IsAdult
    {
        get { return Age >= 18; }
    }


    public Player(string name, DateTime birthDate, decimal balance)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя игрока не может быть пустым.");
        }
        if (balance < 0)
        {
            throw new ArgumentException("Баланс не может быть отрицательным.");
        }

        _id = Guid.NewGuid();
        Name = name.Trim();
        BirthDate = birthDate;
        Balance = balance;
    }

    public Player(string name, DateTime birthDate) : this(name, birthDate, 0m)
    {
    }

    public bool TopUp(decimal amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        Balance += amount;
        return true;
    }

    public bool CanAfford(decimal amount)
    {
        return Balance >= amount;
    }

    public bool TryPay(decimal amount)
    {
        if (amount <= 0 || !CanAfford(amount))
        {
            return false;
        }

        Balance -= amount;
        TicketsBought++;
        return true;
    }

    public void AddWinnings(decimal amount)
    {
        Balance += amount;
        TotalWinnings += amount;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"""
        {Name}, {Age} лет | баланс : {Balance:N2} руб. |
        Билетов куплено : {TicketsBought} | выиграно всего : {TotalWinnings:N2} руб.
        """);
    }
}
