namespace ShoppingCart.Domain.Models.ShoppingCart
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetAsync(string id);
        Task CreateAsync(ShoppingCart item);
        Task<ShoppingCart> UpdateAsync(ShoppingCart item);
        Task<bool> DeleteAsync(string id);
    }
}
