using System.Text.Json;

namespace TumakovTasks;

public class BankAccount : IDisposable
{
    private static uint _lastAccountNumber = 0;
    private uint _accountNumber;
    private decimal _balance;
    private AccountType _accountType;
    private Queue<BankTransaction> _transactions = new Queue<BankTransaction>();
    private bool _disposed = false;

    
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

        _transactions.Enqueue(new BankTransaction(amount));
        return true;
    }

    public bool TryWithdraw(decimal amount)
    {
        if (amount > _balance || amount <= 0)
        {
            return false;
        }

        _balance -= amount;

        _transactions.Enqueue(new BankTransaction(-amount));
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

    public void Dispose()
    {
        if (!_disposed)
        {
            string projectDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName 
                                      ?? AppContext.BaseDirectory;

            string folderPath = Path.Combine(projectDirectory, "Transactions");
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, $"Account_{_accountNumber}_transactions.json");

            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                IncludeFields = true 
            };

            AccountStatement statement;

            if (File.Exists(filePath))
            {
                string existingJson = File.ReadAllText(filePath);
                statement = JsonSerializer.Deserialize<AccountStatement>(existingJson, options) 
                            ?? new AccountStatement();
            }
            else
            {
                statement = new AccountStatement();
            }

            statement.AccountNumber = _accountNumber;
            statement.AccountType = _accountType.ToString();
            statement.GeneratedAt = DateTime.Now;
            statement.FinalBalance = _balance;

            while (_transactions.Count > 0)
            {
                if (_transactions.Dequeue() is BankTransaction transaction)
                {
                    statement.Transactions.Add(transaction);
                }
            }

            string jsonString = JsonSerializer.Serialize(statement, options);
            File.WriteAllText(filePath, jsonString);

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
