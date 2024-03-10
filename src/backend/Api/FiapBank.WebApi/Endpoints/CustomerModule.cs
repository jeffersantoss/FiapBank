using Carter;
using FiapBank.Domain;
using FiapBank.WebApi.Contracts.Requests;
using Microsoft.EntityFrameworkCore;

namespace FiapBank.WebApi.Endpoints;

public class CustomerModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var clientesGroup = app.MapGroup("/clientes");

        // Listar Clientes
        clientesGroup.MapGet("/", async (FiapBankContext context) => {
            var clientes = await context.Customers.Include(x => x.Account).ToListAsync();
            return Results.Ok(clientes.Select(c => new { c.Id, c.Name, c.Account }));
        }).WithTags("Clients").WithName("ListCustomers").WithOpenApi();

        clientesGroup.MapPost("/", async (CustomerRequest request, FiapBankContext context) => {
            if (request == null) return Results.BadRequest("Dados inválidos.");

            Customer customer = new(request.Name, new Account(0));

            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            return Results.Created($"/{customer.Id}", customer);
        }).WithTags("Clients").WithName("CreateCustomer").WithOpenApi();
    }
}