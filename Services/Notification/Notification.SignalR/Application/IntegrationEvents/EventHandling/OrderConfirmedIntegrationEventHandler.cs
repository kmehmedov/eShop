using Microsoft.AspNetCore.SignalR;
using Notification.SignalR.Application.IntegrationEvents.Events;
using Services.Common.Abstractions;

namespace Notification.SignalR.Application.IntegrationEvents.EventHandling
{
    public class OrderConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderConfirmedIntegrationEvent>
    {
        public OrderConfirmedIntegrationEventHandler(
            IHubContext<NotificationHub> hubContext,
            ILogger<OrderConfirmedIntegrationEventHandler> logger)
        {
            _hubContext=hubContext;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderConfirmedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            await _hubContext.Clients
                .Group(@event.BuyerId)
                .SendAsync("UpdatedOrderStatus");
        }

        #region Private members
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<OrderConfirmedIntegrationEventHandler> _logger;
        #endregion
    }
}
