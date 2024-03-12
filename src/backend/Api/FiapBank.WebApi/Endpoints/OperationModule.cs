using Carter;
using FiapBank.Domain;
using FiapBank.WebApi.Contracts.Requests;
using FiapBank.WebApi.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace FiapBank.WebApi.Endpoints;

public class OperationModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Depositar
        app.MapPost("/deposits/{customerId:Guid}", async (Guid customerId, DepositRequest request, FiapBankContext context) =>
        {

            var customer = await context.Customers
                .Include(x => x.CheckingAccount)
                .ThenInclude(x => x.Transactions)
                .Where(x => x.Id == customerId)
                .FirstOrDefaultAsync();

            if (customer == null) return Results.NotFound(ResultResponse.Fail("Cliente não encontrado."));
            if (request.Amount <= 0) return Results.BadRequest(ResultResponse.Fail("O valor do depósito deve ser maior que zero."));
            if (customer.GetAccount() == null) return Results.BadRequest(ResultResponse.Fail("Cliente não possui conta."));

            if (customer.GetAccount().Deposit(request.Amount))
            {
                await context.SaveChangesAsync();
                return Results.Ok(ResultResponse.Ok($"Depósito de R${request.Amount} realizado com sucesso."));
            }
            else
            {
                return Results.BadRequest(ResultResponse.Fail("Erro ao realizar depósito."));
            }

        }).WithTags("Operations").WithName("Deposit").WithOpenApi();

        // Retirar
        app.MapPost("/withdraws/{customerId:Guid}", async (Guid customerId, WithdrawRequest request, FiapBankContext context) =>
        {
            // Buscando o cliente pelo ID. Supondo que cada cliente tenha uma conta associada.
            var customer = await context.Customers.Include(c => c.CheckingAccount).FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null) return Results.NotFound(ResultResponse.Fail("Conta não encontrada."));

            var account = customer.GetAccount();


            if (account == null) return Results.NotFound(ResultResponse.Fail("Conta não associada ao cliente."));

            if (request.Amount <= 0) return Results.BadRequest(ResultResponse.Fail("O valor da retirada deve ser maior que zero."));

            if (account is CheckingAccount chekingAccount && request.Amount > account.GetBalance() + chekingAccount.GetOverdraftLimit())
                return Results.BadRequest(ResultResponse.Fail("Saldo insuficiente."));

            // A operação de retirada agora é feita na instância da conta recuperada do banco de dados.
            if (account.Withdraw(request.Amount))
            {
                await context.SaveChangesAsync(); // Salva as alterações no contexto, persistindo a retirada no banco de dados.
                return Results.Ok(ResultResponse.Ok($"Retirada de R${request.Amount} realizada com sucesso."));
            }
            else
            {
                return Results.BadRequest(ResultResponse.Fail("Saldo insuficiente."));
            }
        }).WithTags("Operations").WithName("Withdraw").WithOpenApi();


        // Saldo
        app.MapGet("/balances/{customerId:Guid}", async (Guid customerId, FiapBankContext context) =>
        {
            var customer = await context.Customers.Include(c => c.CheckingAccount).FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer == null) return Results.NotFound(ResultResponse.Fail("Conta não encontrada."));

            var response = new BalanceResponse
            {
                Balance = customer.GetAccount().GetBalance(),
                OverdraftLimit = customer.GetAccount().GetOverdraftLimit()
            };

            return Results.Ok(ResultResponse<BalanceResponse>.Ok(response));
        }).WithTags("Operations").WithName("GetBalance").WithOpenApi().Produces<BalanceResponse>();

        // Extrato
        app.MapGet("/statements/{customerId:Guid}", async (Guid customerId, FiapBankContext context) =>
        {

            var customer = await context.Customers
                .Include(c => c.CheckingAccount)
                .ThenInclude(a => a.Transactions)
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null) return Results.NotFound("Conta não encontrada.");
            if (customer.GetAccount() == null) return Results.NotFound("Não existe conta associada ao cliente.");

            var account = customer.GetAccount();
            var transactions = account?.GetTransactions() ?? [];

            var response = transactions.Select(t => new TransactionResponse
            {
                Id = t.Id,
                Date = t.Date,
                Amount = t.Amount,
                Type = t.Type
            });

            return Results.Ok(ResultResponse<IEnumerable<TransactionResponse>>.Ok(response));
        }).WithTags("Operations").WithName("GetTransactions").WithOpenApi().Produces<IEnumerable<TransactionResponse>>();
    }
}
