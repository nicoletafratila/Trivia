using Common.ViewModels;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Game.Blazor.Pages
{
    public partial class NewGame
    {
        [Inject]
        protected ICategoryService? CategoryService { get; set; }

        [Inject]
        protected ITriviaGameService? TriviaGameService { get; set; }

        [Parameter]
        public string? CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
                    Game.CategoryId = !string.IsNullOrWhiteSpace(_categoryId) ? int.Parse(_categoryId) : 0;
                    StateHasChanged();
                }
            }
        }
        private string? _categoryId;
        public IEnumerable<CategoryModel>? Categories { get; set; } = new List<CategoryModel>();

        public TriviaGameModel Game { get; set; } = new TriviaGameModel();
        public IEnumerable<TriviaGameModel>? Games { get; set; } = new List<TriviaGameModel>();

        protected override async Task OnInitializedAsync()
        {
            await Refresh();

            int.TryParse(CategoryId, out var categoryId);
            Game.CategoryId = categoryId;
        }

        protected async Task Save()
        {
            Game.CategoryId = !string.IsNullOrWhiteSpace(CategoryId) ? int.Parse(CategoryId) : 0;
            if (TriviaGameService != null)
            {
                await TriviaGameService.SaveAsync(Game);
            }
            await ClearForm();
        }

        protected void Join(TriviaGameModel model)
        {
            Navigation.NavigateTo(@"game\" + model.Id);
        }

        private async Task ClearForm()
        {
            await Refresh();
            CategoryId = "0";
            Game = new TriviaGameModel();
        }

        private async Task Refresh()
        {
            if (CategoryService != null && TriviaGameService != null)
            {
                Categories = await CategoryService.GetAllAsync();
                Games = await TriviaGameService.GetAllAsync();
            }
        }
    }
}
