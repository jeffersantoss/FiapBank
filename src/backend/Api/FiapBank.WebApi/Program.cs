using Carter;
using FiapBank.Domain;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FiapBankContext>(options =>
    options.UseInMemoryDatabase("FiapBankDb"));

builder.Services.AddCarter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "FIAP Bank",
        Version = "v1",
        Description = "API FIAP Bank Endpoints",
        Contact = new() { Name = "FIAP", Email = "https://www.fiap.com.br" }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//}

app.UseSwagger(opt =>
{
    opt.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Contracts API");
    c.DocExpansion(DocExpansion.List);
    c.DefaultModelsExpandDepth(-1);
    c.InjectStylesheet("/swagger-ui/custom.css");
    c.DisplayRequestDuration();
    c.EnableFilter();
    c.EnableValidator();
});

app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapCarter();

// Simulação em memória dos clientes e contas
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FiapBankContext>();
    var customers = new List<Customer>
    {
        new("Jefferson", new Account(1000)),
        new("Beto", new Account(2000)),
        new("Felipe", new Account(3000)),
        new("Henrique", new Account(4000)),
    };
    context.Customers.AddRange(customers);
    context.SaveChanges();
}

app.Run();