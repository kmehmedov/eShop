namespace Order.Domain
{
    public interface IEntityRepository<T> where T : Entity<int>
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByIdsAsync(List<int> ids);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}
