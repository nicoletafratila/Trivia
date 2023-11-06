using Game.Domain.Entities;

namespace Game.Application.Interfaces.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category, int>
    {
    }
}
