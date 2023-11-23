using Microsoft.Extensions.Logging;
using Services.Common.Abstractions;
using Services.Common.Events;

namespace Order.Application.IntegrationEvents
{
    public class OrderIntegrationEventService : IOrderIntegrationEventService
    {
        public OrderIntegrationEventService(
            ILogger<OrderIntegrationEventService> logger,
            IEventBus eventBus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public void PublishThroughEventBus(IntegrationEvent evt)
        {
            try
            {
                _logger.LogInformation("Publishing integration event: {IntegrationEventId_published} - ({@IntegrationEvent})", evt.Id, evt);

                _eventBus.Publish(evt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", evt.Id, evt);
            }
        }

        #region Private members
        private readonly IEventBus _eventBus;
        private readonly ILogger<OrderIntegrationEventService> _logger;
        #endregion
    }
}
