using Game.Blazor.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Game.Blazor
{
    public class AuthHandler : AuthorizationMessageHandler
    {
        public AuthHandler(IAccessTokenProvider provider, NavigationManager navigation, TriviaApiOptions triviaApiOptions, IdentityServerOptions identityServerOptions) : base(provider, navigation)
        {
            ConfigureHandler(new[] { triviaApiOptions.BaseUrl, identityServerOptions.Authority },
                new[] { "profile", "Trivia.API.full", "email", "role", "IdentityServerApi" });
        }
    }
}