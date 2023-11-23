using Notification.Email.Application.IntegrationEvents.Events;
using Services.Common.Abstractions;

namespace Notification.Email.Application.IntegrationEvents.EventHandling
{
    public class OrderConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderConfirmedIntegrationEvent>
    {
        public OrderConfirmedIntegrationEventHandler(
            IEmailSender emailSender,
            ILogger<OrderConfirmedIntegrationEventHandler> logger)
        {
            _emailSender=emailSender;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderConfirmedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            // Send email for confirmed order
            await _emailSender.SendAsync();
        }

        #region Private members
        private readonly IEmailSender _emailSender;
        private readonly ILogger<OrderConfirmedIntegrationEventHandler> _logger;
        #endregion
    }
}
