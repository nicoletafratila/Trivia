using Common.ViewModels;
using Common.ViewModels.Pagination;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Game.Blazor.Pages
{
    public partial class HallOfFame
    {
        public PagedList<UserGameModel>? Users { get; set; }

        [Parameter]
        public QueryParameters? QueryParameters { get; set; } = new();

        [Inject]
        public IHistoryService? HistoryService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (QueryParameters != null)
                {
                    await GetUsers(QueryParameters);
                }
                await base.OnInitializedAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task GetUsers(QueryParameters historyParameters)
        {
            if (QueryParameters == null || HistoryService == null)
            {
                return;
            }

            Users = await HistoryService.GetUsersAsync(historyParameters);
            StateHasChanged();
        }

        private async Task OnPageChangedAsync(int pageNumber)
        {
            if (QueryParameters == null || HistoryService == null)
            {
                return;
            }

            QueryParameters.PageNumber = pageNumber;
            Users = await HistoryService.GetUsersAsync(QueryParameters);
            StateHasChanged();
        }

        //Calculate the the position of each user in the top. 
        //Normally the index would reset on each case so we need to handle multiple cases.
        private int CalculateIndex(int indexFromView)
        {
            var index = 0;

            if (Users is { Metadata.PageNumber: 1 })
            {
                index = indexFromView + 1;
            }
            else
            {
                if (Users != null && Users.Metadata != null)
                {
                    index = Users.Metadata.PageSize * (Users.Metadata.PageNumber - 1) + indexFromView + 1;
                }
            }

            return index;
        }
    }
}
