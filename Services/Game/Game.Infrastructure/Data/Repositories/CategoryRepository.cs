using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;

namespace Game.Infrastructure.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(TriviaDbContext dbContext) : base(dbContext)
        {
        }
    }
}
