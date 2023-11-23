using Notification.Email;
using Notification.Email.Application.IntegrationEvents.EventHandling;
using Notification.Email.Application.IntegrationEvents.Events;
using Services.Common;
using Services.Common.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddCommonServices();
builder.Services.AddControllers();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<OrderCreatedIntegrationEventHandler>();
builder.Services.AddTransient<OrderConfirmedIntegrationEventHandler>();

var app = builder.Build();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
eventBus.Subscribe<OrderConfirmedIntegrationEvent, OrderConfirmedIntegrationEventHandler>();

app.Run();
