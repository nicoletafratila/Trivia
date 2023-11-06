using System.Net.Http.Json;
using Common.ViewModels;
using Game.Blazor.Configuration;
using Game.Blazor.Services.Interfaces;

namespace Game.Blazor.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly TriviaApiOptions _triviaServerOptions;

        public CategoryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _triviaServerOptions = configuration.GetSection(nameof(TriviaApiOptions)).Get<TriviaApiOptions>();
        }

        public async Task<IEnumerable<CategoryModel>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CategoryModel>?>($"{_triviaServerOptions.BaseUrl}/category");
        }
    }
}
