using System.Reflection;
using Ecommerce.Application;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.DbInitializer;
using Ecommerce.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Enable swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Ecommerce API",
        Version = "v1",
        Description = "Shopping cart Web API"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

//add Application Layer
builder.Services.AddApplication();

bool useInMemory = builder.Configuration.GetValue<bool>("useInMemory");

//add Infrastructure Layer
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("EcommerceDb") ?? "EcommerceDb", useInMemory);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API v1");
    });
}

// Seed database
if (useInMemory)
{
    ProductsInitializer.Seed(app.Services);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
