using FiapBank.Domain.Enums;

namespace FiapBank.Domain;

public class Account(decimal balance)
{
    public Guid Id { get; set; }

    public decimal Balance { get; private set; } = balance;

    public List<Transaction> Transactions { get; private set; } = [];

    public bool Deposit(decimal amount)
    {
        try
        {
            Balance += amount;
            Transactions.Add(new Transaction(amount, TransactionType.Deposit));
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public virtual bool Withdraw(decimal amount, decimal overdraftLimit = default)
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

    public decimal GetBalance()
    {
        return Balance;
    }

    public decimal GetOverdraftBalance(decimal overdraftLimit)
    {
        return Balance + overdraftLimit;
    }
}
