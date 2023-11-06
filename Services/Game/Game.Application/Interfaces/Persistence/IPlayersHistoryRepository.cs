using Game.Domain.Entities;

namespace Game.Application.Interfaces.Persistence
{
    public interface IPlayersHistoryRepository : IAsyncRepository<PlayerHistory, long>    
    {
    }
}
