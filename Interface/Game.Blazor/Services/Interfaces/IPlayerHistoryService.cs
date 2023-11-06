using Common.ViewModels;

namespace Game.Blazor.Services.Interfaces
{
    public interface IPlayerHistoryService
    {
        Task SaveAsync(PlayerHistoryModel model);
    }
}
