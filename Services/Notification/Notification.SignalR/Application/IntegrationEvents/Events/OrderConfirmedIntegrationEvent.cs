using Notification.SignalR.Application.Models;
using Services.Common.Events;

namespace Notification.SignalR.Application.IntegrationEvents.Events
{
    public record OrderConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public string BuyerId { get; set; } = string.Empty;
        public IEnumerable<OrderItemDTO> OrderItems { get; } = new List<OrderItemDTO>();
    }
}
