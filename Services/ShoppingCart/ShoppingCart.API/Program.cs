using Services.Common;
using Services.Common.Abstractions;
using ShoppingCart.Application.IntegrationEvents.EventHandling;
using ShoppingCart.Application.IntegrationEvents.Events;
using ShoppingCart.Domain.Models.ShoppingCart;
using ShoppingCart.Infrastructure.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddCommonServices();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton(sp =>
{
    var redisConfig = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);

    return ConnectionMultiplexer.Connect(redisConfig);
});
builder.Services.AddTransient<OrderCreatedIntegrationEventHandler>();
builder.Services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();

app.Run();
