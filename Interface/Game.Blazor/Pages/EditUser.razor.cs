using Common.ViewModels;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Game.Blazor.Pages
{
    public partial class EditUser
    {
        [Inject]
        protected IUsersService? UserService { get; set; }

        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        private IReadOnlyList<IBrowserFile>? _selectedFiles;

        [Parameter]
        public string? UserId { get; set; }

        public ApplicationUserModel? SelectedUser { get; set; } = new ApplicationUserModel();

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(UserId) && UserService != null)
            {
                SelectedUser = await UserService.GetByIdAsync(UserId);
            }
            await base.OnInitializedAsync();
        }

        protected async Task HandleValidSubmit()
        {
            if (SelectedUser != null)
            {
                if (_selectedFiles != null && _selectedFiles.Any())
                {
                    var file = _selectedFiles[0];
                    Stream stream = file.OpenReadStream();
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();
                    SelectedUser.ProfilePicture = ms.ToArray();
                }

                if (UserService != null)
                {
                    await UserService.UpdateAsync(SelectedUser);
                }
                NavigateToOverview();
            }
        }

        protected void HandleInvalidSubmit()
        {
        }

        protected async Task Delete()
        {
            if (SelectedUser != null && !string.IsNullOrWhiteSpace(SelectedUser.UserId))
            {
                if (UserService != null)
                {
                    await UserService.DeleteAsync(SelectedUser.UserId);
                }
                NavigateToOverview();
            }
        }

        protected void NavigateToOverview()
        {
            if (NavigationManager != null)
            {
                NavigationManager.NavigateTo("/manageusers");
            }
        }

        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            _selectedFiles = e.GetMultipleFiles(1);
            StateHasChanged();
        }
    }
}
