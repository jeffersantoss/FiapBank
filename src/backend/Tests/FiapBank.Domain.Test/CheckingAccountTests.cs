namespace FiapBank.Domain.Test;

public class CheckingAccountTests
{
    [Fact]
    public void Withdraw_ShouldAllowOverdraftUpToLimit()
    {
        // Arrange
        var checkingAccount = new CheckingAccount(100, 50);
        var withdrawalAmount = 140;

        // Act
        bool result = checkingAccount.Withdraw(withdrawalAmount);

        // Assert
        Assert.True(result);
        Assert.Equal(-40, checkingAccount.GetBalance());
        Assert.Equal(10, checkingAccount.GetOverdraftLimit());
    }

    [Fact]
    public void Withdraw_ShouldNotAllowOverdraftBeyondLimit()
    {
        // Arrange
        var checkingAccount = new CheckingAccount(100, 50);
        var withdrawalAmount = 160;

        // Act
        bool result = checkingAccount.Withdraw(withdrawalAmount);

        // Assert
        Assert.False(result);
        Assert.Equal(100, checkingAccount.GetBalance());
    }
}
