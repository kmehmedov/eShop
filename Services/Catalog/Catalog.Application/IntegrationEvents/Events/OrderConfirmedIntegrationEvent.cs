using Services.Common.Events;

namespace Catalog.Application.IntegrationEvents.Events
{
    public record OrderConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public IEnumerable<OrderItemDTO> OrderItems { get; }

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
