using Common.ViewModels;

namespace Game.Blazor.Services.Interfaces
{
    public interface ITriviaGameService
    {
        Task SaveAsync(TriviaGameModel game);
        Task<IEnumerable<TriviaGameModel>?> GetAllAsync();
    }
}
