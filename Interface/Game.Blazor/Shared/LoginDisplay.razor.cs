using Game.Blazor.Configuration;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Game.Blazor.Shared
{
    public partial class LoginDisplay
    {
        [Inject]
        protected NavigationManager? Navigation { get; set; }

        [Inject]
        protected SignOutSessionStateManager? SignOutManager { get; set; }

        [Inject]
        protected IUsersService? UserService { get; set; }

        [Inject]
        protected IdentityServerOptions? IdentityServerOptions { get; set; }

        [Inject]
        protected TriviaApiOptions? TriviaApiOptions { get; set; }

        private async Task BeginSignOut(MouseEventArgs args)
        {
            await SignOutManager!.SetSignOutState();
            //The return page after a logout must be the home page
            //Like this, when a member user is logged in after an admin, he will not receive "You are not authorized"
            Navigation!.NavigateTo($"{IdentityServerOptions!.Authority}/account/logout?returnUrl={TriviaApiOptions!.BlazorBaseUrl}");
        }
    }
}
