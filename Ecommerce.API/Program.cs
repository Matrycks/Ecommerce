using Ecommerce.Application;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.DbInitializer;
using Ecommerce.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//add Application Layer
builder.Services.AddApplication();

bool useInMemory = builder.Configuration.GetValue<bool>("useInMemory");

//add Infrastructure Layer
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("EcommerceDb") ?? "EcommerceDb", useInMemory);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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
