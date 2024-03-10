using Carter;
using FiapBank.Domain;
using Microsoft.EntityFrameworkCore;

namespace FiapBank.WebApi.Endpoints;

public class OperationModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Depositar
        app.MapPost("/deposits/{customerId:Guid}/{amount}", async (Guid customerId, double amount, FiapBankContext context) => {

            var customer = await context.Customers
                .Include(x => x.Account)
                .ThenInclude(x => x.Transactions)
                .Where(x => x.Id == customerId)
                .FirstOrDefaultAsync();

            if (customer == null) return Results.NotFound("Conta não encontrada.");

            customer.GetAccount().Deposit(amount);
            await context.SaveChangesAsync();

            return Results.Ok($"Depósito de R${amount} realizado com sucesso.");
        }).WithTags("Operations").WithName("Deposit").WithOpenApi();

        // Retirar
        app.MapPost("/withdraws/{customerId:Guid}/{amount}", async (Guid customerId, double amount, FiapBankContext context) => {
            // Buscando o cliente pelo ID. Supondo que cada cliente tenha uma conta associada.
            var customer = await context.Customers.Include(c => c.Account).FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null) return Results.NotFound("Conta não encontrada.");

            var account = customer.GetAccount();
            if (account == null) return Results.NotFound("Conta não associada ao cliente.");

            // A operação de retirada agora é feita na instância da conta recuperada do banco de dados.
            if (account.Withdraw(amount))
            {
                await context.SaveChangesAsync(); // Salva as alterações no contexto, persistindo a retirada no banco de dados.
                return Results.Ok($"Retirada de R${amount} realizada com sucesso.");
            }
            else
            {
                return Results.BadRequest("Saldo insuficiente.");
            }
        }).WithTags("Operations").WithName("Withdraw").WithOpenApi();


        // Saldo
        app.MapGet("/balances/{customerId:Guid}", async (Guid customerId, FiapBankContext context) => {
            var customer = await context.Customers.Include(c => c.Account).FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer == null) return Results.NotFound("Conta não encontrada.");
            var saldo = customer.GetAccount().GetBalance();
            return Results.Ok($"Saldo: R${saldo}");
        }).WithTags("Operations").WithName("GetBalance").WithOpenApi();

        // Extrato
        app.MapGet("/statements/{customerId:Guid}", async (Guid customerId, FiapBankContext context) => {

            var customer = await context.Customers
                                         .Include(c => c.Account)
                                            .ThenInclude(a => a.Transactions) // Assegura que as transações sejam carregadas
                                         .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null) return Results.NotFound("Conta não encontrada.");

            var account = customer.GetAccount();
            var transactions = account?.GetTransactions();
            if (transactions == null || !transactions.Any())
                return Results.NotFound("Nenhuma transação encontrada.");

            return Results.Ok(transactions.Select(t => new {
                Data = t.Date,
                Tipo = t.Type,
                Valor = t.Amount
            }));
        }).WithTags("Operations").WithName("GetTransactions").WithOpenApi();
    }
}
