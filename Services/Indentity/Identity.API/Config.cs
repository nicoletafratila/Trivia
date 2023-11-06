using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Identity.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { ClaimTypes.Role }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("Trivia.API.full"), new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("Trivia.API","Trivia API")
            {
                ApiSecrets = { new Secret("secret".Sha256()) },
                Scopes = new List<string> { "Trivia.API.full" },
                UserClaims={
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Profile,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Role
                }
            }
        };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "Trivia.Cognizant",
                    ClientName = "Trivia Cognizant",

                    RedirectUris = { "https://localhost:7118/signin-oidc", "https://localhost:7118/", "https://localhost:7118/authentication/login-callback", "https://localhost:7065/signin-oidc", "https://localhost:7118/authentication/signout-callback-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc"},

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "openid", "profile", "email", "Trivia.API.full", "role", IdentityServerConstants.LocalApi.ScopeName},

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding
                }
            };
    }
}