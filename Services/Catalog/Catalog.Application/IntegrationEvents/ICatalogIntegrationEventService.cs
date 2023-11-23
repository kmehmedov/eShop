using Services.Common.Events;

namespace Catalog.Application.IntegrationEvents
{
    public interface ICatalogIntegrationEventService
    {
        void PublishThroughEventBus(IntegrationEvent evt);
    }
}
