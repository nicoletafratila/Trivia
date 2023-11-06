using Common.ViewModels;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace Game.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Inject]
        public ICategoryService? CategoryService { get; set; }

        public IEnumerable<CategoryModel>? Categories { get; set; } = new List<CategoryModel>();

        protected override async Task OnInitializedAsync()
        {
            if (CategoryService != null)
            {
                Categories = await CategoryService.GetAllAsync();
            }
        }

        public void NewGame(int categoryId)
        {
            if (NavigationManager != null)
            {
                NavigationManager.NavigateTo($"/NewGame/{categoryId}");
            }
        }
    }
}
