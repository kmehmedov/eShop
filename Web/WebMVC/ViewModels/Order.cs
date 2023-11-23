namespace WebMVC.ViewModels
{
    public record Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string BuyerId { get; set; }
        public string Status { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalPrice => OrderItems.Sum(x => x.UnitQuantity * x.UnitPrice);
    }
}
