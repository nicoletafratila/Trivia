using Common.ViewModels;

namespace Game.Blazor.Services.Interfaces
{
    public interface IUsersService
    {
        string GetIdentityUrl();
        Task<IEnumerable<ApplicationUserModel>?> GetAllAsync();
        Task<ApplicationUserModel?> GetByIdAsync(string id);
        Task UpdateAsync(ApplicationUserModel user);
        Task DeleteAsync(string id);
    }
}
