using FiapBank.Domain.Enums;

namespace FiapBank.Domain;

public class Transaction
{
    public Transaction() { }

    public Transaction(double amount, TransactionType type)
    {
        Amount = amount;
        Type = type;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Date { get; private set; } = DateTime.Now;
    public double Amount { get; private set; }
    public TransactionType Type { get; private set; }
}
