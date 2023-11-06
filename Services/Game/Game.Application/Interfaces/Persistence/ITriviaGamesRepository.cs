using Game.Domain.Entities;

namespace Game.Application.Interfaces.Persistence;

public interface ITriviaGamesRepository: IAsyncRepository<TriviaGame,long>
{
    Task<IReadOnlyList<TriviaGame>> ListAllGamesIncludeParticipants();
    Task<List<TriviaGame>> GetAllGames();
    Task<TriviaGame?> GetGameByIdIncludeCurrentAndNextQuestion(long id);
}