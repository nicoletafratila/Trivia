using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Game.Blazor.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;
        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        [Inject]
        protected IUsersService? UserService { get; set; }

        [Inject]
        protected NavigationManager? Navigation { get; set; }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private void RedirectToUserProfile()
        {
            if (UserService != null)
            {
                var identityUrl = UserService.GetIdentityUrl();
                Navigation!.NavigateTo($"{identityUrl}/account/profile?returnUrl={Uri.EscapeDataString(Navigation.Uri)}");
            }
        }
    }
}
