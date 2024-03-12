using FiapBank.Domain.Enums;

namespace FiapBank.Domain;

public class CheckingAccount : Account
{
    private CheckingAccount() : base(0)
    {
    }

    public CheckingAccount(decimal initBalance, decimal overdraft) : base(initBalance)
    {
        OverdraftLimit = overdraft;
    }

    public decimal OverdraftLimit { get; private set; }

    public override bool Withdraw(decimal amount, decimal overdraft = 0)
    {
        decimal effectiveBalance = GetBalance() + OverdraftLimit;

        if (amount <= effectiveBalance)
        {
            base.Withdraw(amount, OverdraftLimit);

            if (GetBalance() < 0)
            {
                OverdraftLimit -= Math.Abs(GetBalance());
            }

            return true;
        }

        return false;
    }

    public decimal GetOverdraftLimit()
    {
        return OverdraftLimit;
    }
}