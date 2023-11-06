using Common.ViewModels;
using Common.ViewModels.Pagination;

namespace Game.Blazor.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<IEnumerable<UserGameModel>?> GetUserGamesAsync();
        Task<PagedList<UserGameModel>?> GetUsersAsync(QueryParameters queryStringParameters);
        Task<IEnumerable<UserGameModel>?> GetUserBestStreaksAsync();
    }
}
