using System.Text.Json.Serialization;

namespace TumakovTasks;

public class BankTransaction
{
    [JsonInclude]
    public readonly DateTime TransactionDate;

    [JsonInclude]
    public readonly decimal Amount;

    public BankTransaction(decimal amount)
    {
        TransactionDate = DateTime.Now;
        Amount = amount;
    }

    [JsonConstructor]
    public BankTransaction(DateTime transactionDate, decimal amount)
    {
        TransactionDate = transactionDate;
        Amount = amount;
    }

    public override string ToString()
    {
        string operationType = Amount >= 0 ? "Пополнение" : "Снятие";
        return $"{TransactionDate:dd.MM.yyyy HH:mm:ss} | {operationType} : {Math.Abs(Amount):F2}";
    }
}
