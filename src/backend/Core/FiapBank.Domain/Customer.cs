namespace FiapBank.Domain;

public class Customer
{
    public Customer() { }

    public Customer(string name, Account account)
    {
        Name = name;
        Account = account;
    }
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; private set; } = string.Empty;

    public Account Account { get; private set; } = new Account(0);

    public string GetName() => Name;

    public Account GetAccount() => Account ?? throw new InvalidOperationException("Account not found");

    public void SetName(string value)
    {
        Name = value;
    }
}
