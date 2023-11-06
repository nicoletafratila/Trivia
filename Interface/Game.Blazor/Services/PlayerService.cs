using Game.Blazor.Configuration;
using Game.Blazor.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Game.Blazor.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient _httpClient;
        private readonly TriviaApiOptions _triviaServerOptions;

        public PlayerService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _triviaServerOptions = configuration.GetSection(nameof(TriviaApiOptions)).Get<TriviaApiOptions>();
        }
        public async Task UpdatePerformance(string UserID, bool AnsweredCorrectly, TimeSpan AnswerTime)
        {
            var modelJson = new StringContent(JsonSerializer.Serialize(new { UserID, AnsweredCorrectly, AnswerTime }), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_triviaServerOptions.BaseUrl}/Player", modelJson);
            return;
        }
    }
}
