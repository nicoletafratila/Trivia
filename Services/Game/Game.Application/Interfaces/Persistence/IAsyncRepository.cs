using Game.Domain.Common;

namespace Game.Application.Interfaces.Persistence
{
    public interface IAsyncRepository<T, in TId> where T : Entity<TId>,IAggregateRoot
    {
        Task<T?> GetByIdAsync(TId id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size);
    }
}
