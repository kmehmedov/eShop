using WebMVC.ViewModels;
using WebMVC.ViewModels.ShoppingCartViewModels;

namespace WebMVC.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartAsync(AppUser user);
        Task AddItemToCartAsync(AppUser user, int productId);
        Task<ShoppingCart> RemoveItemFromCartAsync(AppUser user, string itemId);
        Task UpdateCartAsync(ShoppingCart basket);
        Task<ShoppingCart> SetQuantitiesAsync(AppUser user, Dictionary<string, int> quantities);
        //Task Checkout(BasketDTO basket);
        //Task<Order> GetOrderDraft(string basketId);
    }
}
