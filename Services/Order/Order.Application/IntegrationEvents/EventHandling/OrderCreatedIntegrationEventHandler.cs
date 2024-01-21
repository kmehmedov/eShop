using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Order.Application.IntegrationEvents.Events;
using Order.Domain.Exceptions;
using Order.Domain.Models.Orders;
using Services.Common.Abstractions;

namespace Order.Application.IntegrationEvents.EventHandling
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        public OrderCreatedIntegrationEventHandler(IOrderRepository orderRepository, ILogger<OrderCreatedIntegrationEventHandler> logger, IOrderIntegrationEventService orderIntegrationEventService)
        {
            _orderRepository = orderRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderIntegrationEventService=orderIntegrationEventService;
        }
        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            // Simulate some delay
            Thread.Sleep(5000);

            var order = await _orderRepository.GetAsync(@event.OrderId) ?? throw new OrderDomainException($"Can not find order with id - {@event.OrderId}");

            order.Confirm();
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();

            _orderIntegrationEventService.PublishThroughEventBus(new OrderConfirmedIntegrationEvent() { OrderId = order.Id, OrderItems = order.OrderItems.ToOrderItemsDTO(), BuyerId = order.BuyerId });
        }

        #region PRivate members
        private readonly ILogger<OrderCreatedIntegrationEventHandler> _logger;
        private readonly IOrderIntegrationEventService _orderIntegrationEventService;
        private readonly IOrderRepository _orderRepository;
        #endregion
    }
}
