using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Infrastructure;
using ProductApi.Models;
using SharedModels;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ connection string (I use CloudAMQP as a RabbitMQ server).
// Remember to replace this connectionstring with your own.
string connectionString = "host=rabbitmq";

// Add services to the container.

builder.Services.AddDbContext<ProductApiContext>(opt => opt.UseInMemoryDatabase("ProductsDb"));

// Register repositories for dependency injection
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();

// Register database initializer for dependency injection
builder.Services.AddTransient<IDbInitializer, DbInitializer>();

// Register ProductConverter for dependency injection
builder.Services.AddSingleton<IConverter<Product, ProductDto>, ProductConverter>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Initialize the database.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<ProductApiContext>();
    var dbInitializer = services.GetService<IDbInitializer>();
    dbInitializer.Initialize(dbContext);
}

// Create a message listener in a separate thread.
Task.Factory.StartNew(() =>
    new MessageListener(app.Services, connectionString).Start());

//app.UseHttpsRedirection();

app.UseHttpMetrics();

app.UseAuthorization();

app.MapControllers();

app.MapMetrics();

app.Run();

