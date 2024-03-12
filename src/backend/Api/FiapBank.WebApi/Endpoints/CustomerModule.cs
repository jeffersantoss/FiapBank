using Carter;
using FiapBank.Domain;
using FiapBank.WebApi.Contracts.Requests;
using FiapBank.WebApi.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace FiapBank.WebApi.Endpoints;

public class CustomerModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var clientesGroup = app.MapGroup("/clientes");

        // Listar Clientes
        clientesGroup.MapGet("/", async (FiapBankContext context) =>
        {

            var clientes = await context.Customers
            .Include(x => x.CheckingAccount)
            .ToListAsync();

            var response = clientes.Select(c => new ClientResponse
            {
                Id = c.Id,
                Name = c.Name,
                ChekingAccount = new ChekingAccountResponse
                {
                    Balance = c.CheckingAccount.Balance,
                    OverdraftLimit = c.CheckingAccount.OverdraftLimit
                }
            });

            return Results.Ok(ResultResponse<IEnumerable<ClientResponse>>.Ok(response));
        }).WithTags("Clients").WithName("ListCustomers").WithOpenApi().Produces<IEnumerable<ClientResponse>>();

        clientesGroup.MapPost("/", async (CustomerRequest request, FiapBankContext context) =>
        {
            if (request == null) return Results.BadRequest("Dados inválidos.");
            if (string.IsNullOrWhiteSpace(request.Name)) return Results.BadRequest("Nome não pode ser vazio.");

            var customer = Customer.CreateCustomer(request.Name, new CheckingAccount(request.InitialBalance, request.OverdraftLimit));

            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            var response = new ClientResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                ChekingAccount = new ChekingAccountResponse
                {
                    Balance = customer.CheckingAccount.Balance,
                    OverdraftLimit = customer.CheckingAccount.OverdraftLimit
                }
            };

            return Results.Created($"/{customer.Id}", ResultResponse<ClientResponse>.Ok($"Cliente {response.Name} cadastrado com sucesso!", response));
        }).WithTags("Clients").WithName("CreateCustomer").WithOpenApi().Produces<ClientResponse>();
    }
}