namespace Order.Application.Models
{
    public record ShoppingCartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitQuantity { get; set; }
    }
}
