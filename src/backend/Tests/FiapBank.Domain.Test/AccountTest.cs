namespace FiapBank.Domain.Test;

public class AccountTest
{
    [Fact]
    public void Deposit_WithValidAmount_UpdatesBalance()
    {
        // Arrange
        var account = new Account(100);
        var depositAmount = 50;

        // Act
        account.Deposit(depositAmount);

        // Assert
        Assert.Equal(150, account.GetBalance());
    }

    [Fact]
    public void Deposit_WithDrawInsuficientAmount_UpdatesBalance()
    {
        // Arrange
        var account = new Account(50);
        var value = 100;

        // Act
        account.Withdraw(value);

        // Assert
        Assert.Equal(50, account.GetBalance(), 0.001);
    }

    [Fact]
    public void Withdraw_WithSufficientBalance_UpdatesBalance()
    {
        // Arrange
        var account = new Account(100);
        var withdrawAmount = 50;

        // Act
        var result = account.Withdraw(withdrawAmount);

        // Assert
        Assert.True(result);
        Assert.Equal(50, account.GetBalance());
    }
}