namespace FiapBank.Domain;

public class Bank
{
    public Bank() { }

    public Guid Id { get; set; }

    public string Name { get; private set; } = string.Empty;

    private List<Customer> Customers { get; set; } = new();

    public static Bank CreateBank(string name)
    {
        return new Bank
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }

    public void AddCustomer(Customer customer)
    {
        Customers.Add(customer);
    }

    public Customer GetCustomer(int account) => Customers[account];
    public IEnumerable<Customer> GetAllCustomers() => Customers;
}
