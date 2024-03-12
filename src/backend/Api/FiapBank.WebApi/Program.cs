using Carter;
using FiapBank.Domain;
using FiapBank.WebApi;
using FiapBank.WebApi.Filters;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FiapBankContext>(options =>
    options.UseInMemoryDatabase("FiapBankDb"));

builder.Services.AddCarter();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

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

    options.DescribeAllParametersInCamelCase();
    options.SchemaFilter<EnumSchemaFilter>();

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

app.UseCors();

app.UseSwagger(opt =>
{
    opt.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Contracts API");
    c.DocExpansion(DocExpansion.List);
    c.DefaultModelExpandDepth(3);
    c.DefaultModelsExpandDepth(2);
    c.InjectStylesheet("/swagger-ui/custom.css");
    c.DisplayRequestDuration();
    c.DefaultModelRendering(ModelRendering.Model);
    c.EnableFilter();
    c.EnableValidator();
});

app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapCarter();

app.InitialSeed();

app.Run();