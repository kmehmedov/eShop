using Order.Application.Models;
using Services.Common.Events;

namespace Order.Application.IntegrationEvents.Events
{
    public record OrderConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; init; }
        public IEnumerable<OrderItemDTO> OrderItems { get; init; }

        //public OrderConfirmedIntegrationEvent(int orderId,
        //    IEnumerable<OrderItemDTO> orderStockItems)
        //{
        //    OrderId = orderId;
        //    OrderItems = orderStockItems;
        //}
    }
}
