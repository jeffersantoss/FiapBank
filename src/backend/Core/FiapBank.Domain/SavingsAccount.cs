namespace FiapBank.Domain;

public class SavingsAccount : Account
{
    public SavingsAccount() : base(0) { }

    public SavingsAccount(double initBalance, double interestRate) : base(initBalance)
    {
        InterestRate = interestRate;
    }

    public double InterestRate { get; private set; }

    public void AddInterest()
    {
        double interest = GetBalance() * InterestRate / 100;
        Deposit(interest);
    }
}