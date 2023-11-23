using Services.Common.Events;

namespace Order.Application.IntegrationEvents
{
    public interface IOrderIntegrationEventService
    {
        void PublishThroughEventBus(IntegrationEvent evt);
    }
}
