using Bogus;
using FiapBank.Domain;

namespace FiapBank.WebApi;

public static class Seed
{
    /// <summary>
    /// Simulação em memória dos clientes e contas usando Bogus onde os clientes terão um nome e uma conta com saldo inicial aleatórp e um limite de cheque especial também aleatório.
    /// </summary>
    public static void InitialSeed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<FiapBankContext>();

        var customers = new Faker<Customer>()
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.CheckingAccount, f => new CheckingAccount(f.Finance.Amount(1000, 10000), f.Finance.Amount(1000, 10000)))
            .Generate(50);

        context.Customers.AddRange(customers);
        context.SaveChanges();
    }
}
