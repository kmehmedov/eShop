using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.API;
using Order.Application.Commands;
using Order.Application.IntegrationEvents;
using Order.Application.IntegrationEvents.EventHandling;
using Order.Application.IntegrationEvents.Events;
using Order.Application.Models;
using Order.Application.Queries;
using Order.Domain.Models.Orders;
using Order.Infrastructure;
using Order.Infrastructure.Repositories;
using Services.Common;
using Services.Common.Abstractions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddCommonServices();
builder.Services.AddControllers();
builder.Services.AddDbContext<OrderContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("OrderDb");
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.MigrationsAssembly(typeof(OrderContext).Assembly.FullName);
        sqlServerOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });
});
builder.Services.Configure<OrderSettings>(builder.Configuration);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = "Please refer to the errors property for additional details."
        };

        return new BadRequestObjectResult(problemDetails)
        {
            ContentTypes = { "application/problem+json", "application/problem+xml" }
        };
    };
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderIntegrationEventService, OrderIntegrationEventService>();
builder.Services.AddTransient<OrderCreatedIntegrationEventHandler>();
builder.Services.AddTransient<OrderConfirmedIntegrationEventHandler>();

builder.Services.AddScoped<IRequestHandler<CancelOrderCommand, CommandResult<bool>>, CancelOrderCommandHandler>();
builder.Services.AddScoped<IRequestHandler<ConfirmOrderCommand, CommandResult<bool>>, ConfirmOrderCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateOrderCommand, CommandResult<OrderDTO>>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateOrderFromShoppingCartCommand, CommandResult<OrderDTO>>, CreateOrderFromShoppingCartCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetOrderByIdQuery, QueryResult<OrderDTO>>, GetOrderByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetOrdersQuery, QueryResult<IEnumerable<OrderDTO>>>, GetOrdersQueryHandler>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
eventBus.Subscribe<OrderConfirmedIntegrationEvent, OrderConfirmedIntegrationEventHandler>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OrderContext>();
    await context.Database.MigrateAsync();
}

await app.RunAsync();
