namespace TumakovTasks;

public class AccountStatement
{
    public uint AccountNumber { get; set; }
    public string AccountType { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; }
    public decimal FinalBalance { get; set; }
    public List<BankTransaction> Transactions { get; set; } = new();
}
