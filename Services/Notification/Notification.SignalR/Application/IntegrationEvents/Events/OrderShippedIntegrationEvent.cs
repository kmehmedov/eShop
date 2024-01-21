using Notification.SignalR.Application.Models;
using Services.Common.Events;
using System.Text.Json.Serialization;

namespace Notification.SignalR.Application.IntegrationEvents.Events
{
    public record OrderShippedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public string BuyerId { get; }
        public IEnumerable<OrderItemDTO> OrderItems { get; }

        [JsonConstructor]
        public OrderShippedIntegrationEvent(int orderId, string buyerId, IEnumerable<OrderItemDTO> orderStockItems)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            OrderItems = orderStockItems;
        }
    }
}
