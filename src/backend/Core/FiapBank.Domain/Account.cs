using FiapBank.Domain.Enums;

namespace FiapBank.Domain;

public class Account(double balance)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public double Balance { get; private set; } = balance;

    public List<Transaction> Transactions { get; private set; } = [];

    public void Deposit(double amount)
    {
        Balance += amount;
        Transactions.Add(new Transaction(amount, TransactionType.Deposit));
    }

    public virtual bool Withdraw(double amount, double overdraftLimit = default)
    {
        bool result = false;
        if (amount <= Balance + overdraftLimit)
        {
            Balance -= amount;
            Transactions.Add(new Transaction(amount, TransactionType.Withdraw));
            result = true;
        }
        return result;
    }

    public IEnumerable<Transaction> GetTransactions() => Transactions;

    public double GetBalance()
    {
        return Balance;
    }

    public double GetOverdraftBalance(double overdraftLimit)
    {
        return Balance + overdraftLimit;
    }
}
