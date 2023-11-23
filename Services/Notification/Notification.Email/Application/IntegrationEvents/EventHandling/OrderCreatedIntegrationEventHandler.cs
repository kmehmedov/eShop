using Notification.Email.Application.IntegrationEvents.Events;
using Services.Common.Abstractions;

namespace Notification.Email.Application.IntegrationEvents.EventHandling
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        public OrderCreatedIntegrationEventHandler(
            IEmailSender emailSender,
            ILogger<OrderCreatedIntegrationEventHandler> logger)
        {
            _emailSender=emailSender;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            // Send email for created order
            await _emailSender.SendAsync();
        }

        #region Private members
        private readonly IEmailSender _emailSender;
        private readonly ILogger<OrderCreatedIntegrationEventHandler> _logger;
        #endregion
    }
}
