namespace FiapBank.Domain;

public class SavingsAccount : Account
{
    public SavingsAccount() : base(0) { }

    public SavingsAccount(decimal initBalance, decimal interestRate) : base(initBalance)
    {
        InterestRate = interestRate;
    }

    public decimal InterestRate { get; private set; }

    public void AddInterest(string description)
    {
        decimal interest = GetBalance() * InterestRate / 100;
        Deposit(interest);
    }
}