namespace TumakovTasks;

public class BankAccount
{
    private static uint _lastAccountNumber = 0;
    private uint _accountNumber;
    private decimal _balance;
    private AccountType _accountType;

    
    public uint AccountNumber => _accountNumber;

    private static uint GenerateNextNumber()
    {
        _lastAccountNumber++;
        return _lastAccountNumber;
    }

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
        _accountNumber = GenerateNextNumber();
        Balance = initialBalance;
        AccountType = accountType;
    }

    public bool TryDeposit(decimal amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        _balance += amount;
        return true;
    }

    public bool TryWithdraw(decimal amount)
    {
        if (amount > _balance || amount <= 0)
        {
            return false;
        }

        _balance -= amount;
        return true;
    }

    public bool TryMoneyTransfer(BankAccount sourceBankAccount, decimal transferAmount)
    {
        if (sourceBankAccount == null)
        {
            throw new ArgumentNullException("Ошибка: Счёт, с которого снимаются деньги, не найден.");
        }

        if (ReferenceEquals(sourceBankAccount, this))
        {
            throw new Exception("Ошибка: Нельзя перевести деньги на свой же счёт.");
        }

        if (!sourceBankAccount.TryWithdraw(transferAmount))
        {
            return false;
        }
 
        TryDeposit(transferAmount);
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
