using FiapBank.Domain.Enums;

namespace FiapBank.Domain;

public class CheckingAccount : Account
{
    private CheckingAccount() : base(0)
    {
    }

    public CheckingAccount(double initBalance, double overdraft) : base(initBalance)
    {
        OverdraftLimit = overdraft;
    }

    public double OverdraftLimit { get; private set; }

    public override bool Withdraw(double amount, double overdraft = 0)
    {
        double effectiveBalance = GetBalance() + OverdraftLimit;

        if (amount <= effectiveBalance)
        {
            base.Withdraw(amount, OverdraftLimit);

            if (GetBalance() < 0)
            {
                OverdraftLimit -= Math.Abs(GetBalance());
                base.Transactions.Add(new Transaction(Math.Abs(GetBalance()), TransactionType.Overdraft));
            }

            return true;
        }

        return false;
    }

    public double GetOverdraftLimit()
    {
        return OverdraftLimit;
    }
}