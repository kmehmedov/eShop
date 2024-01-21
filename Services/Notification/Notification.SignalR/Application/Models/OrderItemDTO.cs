namespace Notification.SignalR.Application.Models
{
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
