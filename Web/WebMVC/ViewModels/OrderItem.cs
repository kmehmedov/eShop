namespace WebMVC.ViewModels
{
    public record OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitQuantity { get; set; }
    }
}
