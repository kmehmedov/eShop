﻿using Order.Application.Models;
using Services.Common.Events;

namespace Order.Application.IntegrationEvents.Events
{
    public record OrderShippedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public string BuyerId { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; } = new List<OrderItemDTO>();

        public OrderShippedIntegrationEvent(int orderId, string buyerId, IEnumerable<OrderItemDTO> orderStockItems)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            OrderItems = orderStockItems;
        }
    }
}
