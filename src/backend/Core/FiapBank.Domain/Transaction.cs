using FiapBank.Domain.Enums;

namespace FiapBank.Domain;

public class Transaction
{
    public Transaction() { }

    public Transaction(decimal amount, TransactionType type)
    {
        Amount = amount;
        Type = type;
    }

    public Guid Id { get; set; }
    public DateTime Date { get; private set; } = DateTime.Now;
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
}
