using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Data.Repositories;

public class TriviaGamesRepository : BaseRepository<TriviaGame, long>, ITriviaGamesRepository
{
    public TriviaGamesRepository(TriviaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<TriviaGame>> ListAllGamesIncludeParticipants()
    {
        return await DbContext.Games
           .Include(x => x.GameParticipants).ToListAsync();
    }

    public async Task<TriviaGame?> GetGameByIdIncludeCurrentAndNextQuestion(long id)
    {
        return await DbContext.Games
            .Include(x => x.CurrentGameQuestion)
            .ThenInclude(x => x!.NextGameQuestion)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TriviaGame>> GetAllGames()
    {
        return await DbContext.Games.ToListAsync();
    }
}