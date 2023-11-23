using Catalog.Application.IntegrationEvents.Events;
using Catalog.Domain.Models.CatalogItems;
using Catalog.Infrastructure;
using Microsoft.Extensions.Logging;
using Services.Common.Abstractions;

namespace Catalog.Application.IntegrationEvents.EventHandling
{
    public class OrderConfirmedIntegrationEventHandler :
        IIntegrationEventHandler<OrderConfirmedIntegrationEvent>
    {
        public OrderConfirmedIntegrationEventHandler(
            CatalogContext catalogContext,
            ICatalogItemRepository catalogItemRepository,
            ILogger<OrderConfirmedIntegrationEventHandler> logger)
        {
            _catalogContext = catalogContext;
            _catalogItemRepository = catalogItemRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderConfirmedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            //we're not blocking stock/inventory
            foreach (var orderStockItem in @event.OrderItems)
            {
                var catalogItem = await _catalogItemRepository.GetAsync(orderStockItem.ProductId);

                catalogItem.RemoveQuantity(orderStockItem.UnitQuantity);
            }

            await _catalogContext.SaveChangesAsync();
        }

        #region Private members
        private readonly CatalogContext _catalogContext;
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly ILogger<OrderConfirmedIntegrationEventHandler> _logger;
        #endregion
    }
}
