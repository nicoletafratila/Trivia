using Game.Domain.Entities;

namespace Game.Application.Interfaces.Persistence
{
    public interface IGameParticipantsRepository : IAsyncRepository<GameParticipant, long>
    {
        Task<IEnumerable<GameParticipant>> GetGameParticipantsRanksByTriviaGameId(long triviaGameId);

        Task ResetScoreForEachGameParticipantInTriviaGame(long triviaGameId);
    }
}
