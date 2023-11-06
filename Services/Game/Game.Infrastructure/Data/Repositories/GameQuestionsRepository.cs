using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;

namespace Game.Infrastructure.Data.Repositories
{
    public class GameQuestionsRepository : BaseRepository<GameQuestion, long>, IGameQuestionsRepository
    {
        public GameQuestionsRepository(TriviaDbContext dbContext) : base(dbContext)
        {
        }
    }
}
