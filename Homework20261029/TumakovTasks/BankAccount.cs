namespace TumakovTasks;

public class BankAccount
{
    private Guid _accountNumber;
    private decimal _balance;
    private AccountType _accountType;

    public Guid AccountNumber => _accountNumber;

    public decimal Balance
    {
        get { return _balance; }
        set 
        {
            if (value < 0)
                throw new ArgumentException("Ошибка : Баланс не может быть отрицательным.");
            _balance = value;
        }
    }

    public AccountType AccountType
    {
        get { return _accountType; }
        set { _accountType = value; }
    }

    public BankAccount(decimal initialBalance, AccountType accountType)
    {
        _accountNumber = Guid.NewGuid();         
        Balance = initialBalance;
        AccountType = accountType;
    }

    public void Deposit(decimal amount)
    {
        _balance += amount;
    }

    public bool TryWithdraw(decimal amount)
    {
        if (amount > _balance)
        {
            return false;
        }

        _balance -= amount;
        return true;
    }

    public void PrintInfo()
    {
        Console.WriteLine($""""
        Номер счёта : {_accountNumber} ;
        Баланс : {_balance:F2} ;
        Тип счёта : {_accountType} .
        """");
    }
}
