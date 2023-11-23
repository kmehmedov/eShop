using Order.Application.Models;
using Services.Common.Events;

namespace Order.Application.IntegrationEvents.Events
{
    public record OrderShippedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public IEnumerable<OrderItemDTO> OrderItems { get; }

        public OrderShippedIntegrationEvent(int orderId,
            IEnumerable<OrderItemDTO> orderStockItems)
        {
            OrderId = orderId;
            OrderItems = orderStockItems;
        }
    }
}
