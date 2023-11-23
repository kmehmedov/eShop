namespace ShoppingCart.Domain.Models.ShoppingCart
{
    public class ShoppingCart
    {
        public ShoppingCart(string buyerId)
        {
            BuyerId = buyerId;
        }
        public string BuyerId { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new();
    }
}
