using Common.ViewModels;
using Game.Blazor.Interfaces;
using Game.Blazor.Services;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;

namespace Game.Blazor.Pages
{
    public partial class ManageUsers
    {
        [Inject]
        protected IWriterService? CSVWriterService { get; set; }

        [Inject]
        protected IUsersService? UserService { get; set; }

        public ObservableCollection<ApplicationUserModel>? Users { get; set; }
        public ApplicationUserModel SelectedUser { get; private set; } = new ApplicationUserModel();

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            await base.OnInitializedAsync();
        }

        protected async Task DeleteAsync(string userId)
        {
            if (UserService != null)
            {
                await UserService.DeleteAsync(userId);
            }
            await LoadDataAsync();
        }

        public async void UpdateAsync(ApplicationUserModel user, object checkedValue)
        {
            user.IsActive = (bool)checkedValue;
            if (UserService != null)
            {
                await UserService.UpdateAsync(user);
            }
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            Users = new ObservableCollection<ApplicationUserModel>();
            if (UserService != null)
            {
                var items = await UserService.GetAllAsync();
                if (items != null)
                {
                    items.ToList().ForEach(item => Users.Add(item));
                }
            }
            StateHasChanged();
        }

        private async Task DownloadFileFromStreamAsync()
        {
            if (UserService != null && CSVWriterService != null)
            {
                var users = await UserService.GetAllAsync();
                if (users != null)
                {
                    var fileStream = CSVWriterService.GetUserRecords(users);
                    var fileName = $"userReport-{DateTime.UtcNow}.csv";
                    await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, Convert.ToBase64String(fileStream.ToArray()));
                }
            }
        }
    }
}

