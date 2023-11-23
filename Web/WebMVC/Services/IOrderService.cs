using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public interface IOrderService
    {
        Task<Order> CreateDraftFromShoppinCartAsync(ShoppingCart shoppingCart);
        Task<Order> Create(Order order);
        Task Cancel(int id);
        Task<IEnumerable<Order>> GetAll(AppUser user);
    }
}
