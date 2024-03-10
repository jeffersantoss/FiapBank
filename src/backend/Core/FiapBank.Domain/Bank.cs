namespace FiapBank.Domain;

public class Bank
{
    public Bank() { }

    public Bank(string name)
    {
        Name = name;
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; private set; } = string.Empty;

    private List<Customer> Customers { get; set; } = new();

    public void AddCustomer(Customer customer)
    {
        Customers.Add(customer);
    }

    public Customer GetCustomer(int account) => Customers[account];
    public IEnumerable<Customer> GetAllCustomers() => Customers;
}
