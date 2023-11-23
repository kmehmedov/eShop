namespace WebMVC.ViewModels
{
    public record ShoppingCartItem
    {
        public string Id { get; set; }
        public int ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal UnitPrice { get; init; }
        public int UnitQuantity { get; set; }
    }
}
