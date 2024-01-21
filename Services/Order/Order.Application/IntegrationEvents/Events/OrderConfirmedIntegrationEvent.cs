using Order.Application.Models;
using Services.Common.Events;

namespace Order.Application.IntegrationEvents.Events
{
    public record OrderConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; init; }
        public string BuyerId { get; set; } = string.Empty;
        public IEnumerable<OrderItemDTO> OrderItems { get; init; } = new List<OrderItemDTO>();
    }
}
