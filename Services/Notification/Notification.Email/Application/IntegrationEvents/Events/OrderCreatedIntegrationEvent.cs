﻿using Services.Common.Events;

namespace Notification.Email.Application.IntegrationEvents.Events
{
    public record OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public OrderCreatedIntegrationEvent(string buyerId, int orderId)
        {
            BuyerId = buyerId;
            OrderId = orderId;
        }
        public string BuyerId { get; set; }
        public int OrderId { get; set; }
    }
}
