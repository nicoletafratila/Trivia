using Common.ViewModels;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Game.Blazor.Pages
{
    public partial class History
    {
        public IEnumerable<UserGameModel>? BestStreaks { get; set; } = new List<UserGameModel>();
        public IEnumerable<UserGameModel>? UserGames { get; set; } = new List<UserGameModel>();

        [Inject]
        protected IHistoryService? HistoryService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (HistoryService != null)
            {
                UserGames = await HistoryService.GetUserGamesAsync();
                BestStreaks = await HistoryService.GetUserBestStreaksAsync();
            }
            await base.OnInitializedAsync();
        }
    }
}
