namespace Order.Domain.Models.Orders
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        Task<List<Order>> GetByBuyerIdAsync(string buyerId);
    }
}
