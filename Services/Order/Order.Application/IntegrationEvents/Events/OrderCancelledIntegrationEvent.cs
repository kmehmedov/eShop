using Services.Common.Events;

namespace Order.Application.IntegrationEvents.Events
{
    public record OrderCancelledIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public OrderCancelledIntegrationEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
