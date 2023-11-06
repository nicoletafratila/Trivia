using Common.ViewModels;

namespace Game.Blazor.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>?> GetAllAsync();
    }
}
