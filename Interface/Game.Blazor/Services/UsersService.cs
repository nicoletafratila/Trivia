using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Common.ViewModels;
using Game.Blazor.Configuration;
using Game.Blazor.Services.Interfaces;

namespace Game.Blazor.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly IdentityServerOptions _identityServerOptions;

        public UsersService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _identityServerOptions = configuration.GetSection(nameof(IdentityServerOptions)).Get<IdentityServerOptions>();
        }

        public string GetIdentityUrl()
        {
            return _identityServerOptions.Authority;
        }

        public async Task<IEnumerable<ApplicationUserModel>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ApplicationUserModel>?>($"{_identityServerOptions.Authority}/users");
        }

        public async Task<ApplicationUserModel?> GetByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<ApplicationUserModel?>($"{_identityServerOptions.Authority}/users/{id}");
        }

        public async Task UpdateAsync(ApplicationUserModel user)
        {
            var userJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_identityServerOptions.Authority}/users", userJson);
        }

        public async Task DeleteAsync(string id)
        {
            await _httpClient.DeleteAsync($"{_identityServerOptions.Authority}/users/{id}");
        }
    }
}
