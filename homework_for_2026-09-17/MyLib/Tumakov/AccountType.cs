namespace MyLib.Tumakov;

public struct BankAccount
{
    public string AccountNumber;
    public AccountType Type;
    public decimal Balance;

    public BankAccount(string number, AccountType type, decimal balance)
    {
        AccountNumber = number;
        Type = type;
        Balance = balance;
    }
}

public enum AccountType
{
    Current,  
    Savings   
}
