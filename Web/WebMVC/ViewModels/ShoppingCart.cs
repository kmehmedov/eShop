namespace WebMVC.ViewModels
{
    public record ShoppingCart
    {
        public ShoppingCart(string buyerId)
        {
            BuyerId = buyerId;
        }
        public List<ShoppingCartItem> Items { get; init; } = new List<ShoppingCartItem>();
        public string BuyerId { get; init; }
        public decimal TotalPrice => Items.Sum(x => x.UnitQuantity * x.UnitPrice);
    }
}
