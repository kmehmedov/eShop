using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories
{
    public abstract class EntityRepositoryBase<T> : IEntityRepository<T>
            where T : Entity<int>
    {
        public EntityRepositoryBase(CatalogContext context)
        {
            this._context = context;
        }

        public async Task CreateAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().SingleAsync(x => x.Id == id);
        }
        public async Task<List<T>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Set<T>().Where(x => ids.Any(id => id == x.Id)).ToListAsync();
        }
        public async Task UpdateAsync(T item)
        {
            _context.Set<T>().Update(item);
            await Task.CompletedTask;
        }

        #region Private members
        private readonly CatalogContext _context;
        #endregion
    }
}
