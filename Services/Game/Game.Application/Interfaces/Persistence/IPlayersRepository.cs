using Game.Domain.Entities;

namespace Game.Application.Interfaces.Persistence
{
    public interface IPlayersRepository : IAsyncRepository<Player, long>
    {
        Task<Player> UpdatePlayerPerformance(string UserId, bool AnsweredCorrectly, TimeSpan AnswerTime);

        Task<Player> GetPlayerFromDB(string UserId);
    }
}
