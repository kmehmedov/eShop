using Microsoft.Extensions.Logging;
using Services.Common.Abstractions;
using ShoppingCart.Application.IntegrationEvents.Events;
using ShoppingCart.Domain.Models.ShoppingCart;

namespace ShoppingCart.Application.IntegrationEvents.EventHandling
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        public OrderCreatedIntegrationEventHandler(IShoppingCartRepository shoppingCartRepository, ILogger<OrderCreatedIntegrationEventHandler> logger)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            await _shoppingCartRepository.DeleteAsync(@event.BuyerId.ToString());
        }

        #region PRivate members
        private readonly ILogger<OrderCreatedIntegrationEventHandler> _logger;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        #endregion
    }
}
