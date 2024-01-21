using Microsoft.Extensions.Logging;
using Order.Application.IntegrationEvents.Events;
using Order.Domain.Exceptions;
using Order.Domain.Models.Orders;
using Services.Common.Abstractions;

namespace Order.Application.IntegrationEvents.EventHandling
{
    public class OrderConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderConfirmedIntegrationEvent>
    {
        public OrderConfirmedIntegrationEventHandler(IOrderRepository orderRepository, ILogger<OrderConfirmedIntegrationEventHandler> logger, IOrderIntegrationEventService orderIntegrationEventService)
        {
            _orderRepository = orderRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderIntegrationEventService=orderIntegrationEventService;
        }
        public async Task Handle(OrderConfirmedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            // Simulate some delay
            Thread.Sleep(5000);

            var order = await _orderRepository.GetAsync(@event.OrderId) ?? throw new OrderDomainException($"Can not find order with id - {@event.OrderId}");

            order.Ship();
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();

            _orderIntegrationEventService.PublishThroughEventBus(new OrderShippedIntegrationEvent(order.Id, order.BuyerId, order.OrderItems.ToOrderItemsDTO()));
        }

        #region PRivate members
        private readonly ILogger<OrderConfirmedIntegrationEventHandler> _logger;
        private readonly IOrderIntegrationEventService _orderIntegrationEventService;
        private readonly IOrderRepository _orderRepository;
        #endregion
    }
}
