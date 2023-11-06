using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Data.Repositories
{


    public class GameParticipantsRepository : BaseRepository<GameParticipant, long>, IGameParticipantsRepository
    {
        public GameParticipantsRepository(TriviaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<GameParticipant>> GetGameParticipantsRanksByTriviaGameId(long triviaGameId)
        {
            return await DbContext.GameParticipants
                .Where(g => g.TriviaGameId == triviaGameId)
                .OrderByDescending(g => g.Score)
                .ToListAsync();
        }

        public async Task ResetScoreForEachGameParticipantInTriviaGame(long triviaGameId)
        {
             DbContext.GameParticipants
                .Where(g => g.TriviaGameId == triviaGameId)
                .ToList()
                .ForEach(g => g.Score = 0);
             await DbContext.SaveChangesAsync();
        }
    }
}
