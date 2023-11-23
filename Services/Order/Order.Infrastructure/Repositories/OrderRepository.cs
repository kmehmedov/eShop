using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Domain.Models.Orders;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(OrderContext context)
        {
            this._context = context;
        }
        public IUnitOfWork UnitOfWork => this._context;

        public async Task CreateAsync(Domain.Models.Orders.Order item)
        {
            await _context.Orders.AddAsync(item);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                _context.Orders.Remove(entity);
            }
        }
        public async Task<List<Domain.Models.Orders.Order>> GetAllAsync()
        {
            return await _context.Orders.Include(x => x.OrderItems).ToListAsync();
        }
        public async Task<Domain.Models.Orders.Order> GetAsync(int id)
        {
            return await _context.Orders.Include(x => x.OrderItems).SingleAsync(x => x.Id == id);
        }
        public async Task<List<Domain.Models.Orders.Order>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Orders.Include(x => x.OrderItems).Where(x => ids.Any(id => id == x.Id)).ToListAsync();
        }
        public async Task UpdateAsync(Domain.Models.Orders.Order item)
        {
            _context.Orders.Update(item);
            await Task.CompletedTask;
        }
        public async Task<List<Domain.Models.Orders.Order>> GetByBuyerIdAsync(string buyerId)
        {
            return await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == buyerId).ToListAsync();
        }

        #region Private members
        private readonly OrderContext _context;
        #endregion
    }
}
