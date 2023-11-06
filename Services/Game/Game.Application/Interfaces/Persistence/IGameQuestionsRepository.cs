using Game.Domain.Entities;

namespace Game.Application.Interfaces.Persistence
{
    public interface IGameQuestionsRepository : IAsyncRepository<GameQuestion, long>
    {
    }
}
