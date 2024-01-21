using Notification.SignalR;
using Notification.SignalR.Application.IntegrationEvents.EventHandling;
using Notification.SignalR.Application.IntegrationEvents.Events;
using Services.Common;
using Services.Common.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCommonServices();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddTransient<OrderConfirmedIntegrationEventHandler>();
builder.Services.AddTransient<OrderShippedIntegrationEventHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseAuthentication();
// TODO: Implement authorization
//app.UseAuthorization();

app.MapHub<NotificationHub>("hub/notificationhub");

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<OrderConfirmedIntegrationEvent, OrderConfirmedIntegrationEventHandler>();
eventBus.Subscribe<OrderShippedIntegrationEvent, OrderShippedIntegrationEventHandler>();

app.Run();
