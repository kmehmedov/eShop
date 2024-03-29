﻿using Services.Common.Events;

namespace Notification.Email.Application.IntegrationEvents.Events
{
    public record OrderConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public string BuyerId { get; set; } = string.Empty;
        public IEnumerable<OrderItemDTO> OrderItems { get; } = new List<OrderItemDTO>();

        //public OrderConfirmedIntegrationEvent(int orderId,
        //    IEnumerable<OrderItemDTO> orderStockItems)
        //{
        //    OrderId = orderId;
        //    OrderItems = orderStockItems;
        //}
    }

    public record OrderItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitQuantity { get; set; }
        public int? OrderId { get; set; }
    }
}
