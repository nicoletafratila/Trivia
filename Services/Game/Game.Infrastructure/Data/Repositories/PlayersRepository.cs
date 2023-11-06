using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Data.Repositories
{
    public class PlayersRepository : BaseRepository<Player, long>, IPlayersRepository
    {
        public PlayersRepository(TriviaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Player> UpdatePlayerPerformance(string UserId, bool AnsweredCorrectly, TimeSpan AnswerTime)
        {
            var player = await GetPlayerFromDB(UserId);
            player.UpdatePerformance(AnsweredCorrectly, AnswerTime);
            DbContext.Players.Update(player);
            DbContext.SaveChanges();
            return player;
        }

        public async Task<Player> GetPlayerFromDB(string UserId)
        {
            return await DbContext.Players.FirstAsync(p => p.UserId == UserId);
        }
    }
}
