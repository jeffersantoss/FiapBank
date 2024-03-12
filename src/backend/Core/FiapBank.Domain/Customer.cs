namespace FiapBank.Domain;

public class Customer
{
    public Customer() { }

    public Guid Id { get; set; }

    public string Name { get; private set; } = string.Empty;

    public CheckingAccount CheckingAccount { get; private set; } = new CheckingAccount(0, 0);

    public string GetName() => Name;

    public CheckingAccount GetAccount() => CheckingAccount ?? throw new InvalidOperationException("Checking Account not found");

    public void SetName(string value)
    {
        Name = value;
    }

    public static Customer CreateCustomer(string name, CheckingAccount checkingAccount)
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            Name = name,
            CheckingAccount = checkingAccount ?? throw new ArgumentNullException(nameof(checkingAccount))
        };
    }
}
