using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;

namespace Game.Infrastructure.Data.Repositories
{
    public class PlayersHistoryRepository : BaseRepository<PlayerHistory, long>, IPlayersHistoryRepository
    {
        public PlayersHistoryRepository(TriviaDbContext dbContext) : base(dbContext)
        {
        }
    }
}
