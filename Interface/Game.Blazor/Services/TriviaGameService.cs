using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Common.ViewModels;
using Game.Blazor.Configuration;
using Game.Blazor.Services.Interfaces;

namespace Game.Blazor.Services
{
    public class TriviaGameService : ITriviaGameService
    {
        private readonly HttpClient _httpClient;
        private readonly TriviaApiOptions _triviaServerOptions;

        public TriviaGameService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _triviaServerOptions = configuration.GetSection(nameof(TriviaApiOptions)).Get<TriviaApiOptions>();
        }

        public async Task<IEnumerable<TriviaGameModel>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TriviaGameModel>?>($"{_triviaServerOptions.BaseUrl}/triviagame");
        }

        public async Task SaveAsync(TriviaGameModel game)
        {
            var modelJson = new StringContent(JsonSerializer.Serialize(new { game.Name, GameType = game.Type, game.CategoryId }), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_triviaServerOptions.BaseUrl}/triviagame", modelJson);
            return;
        }
    }
}
