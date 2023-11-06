using Common.ViewModels;
using Game.Blazor.Configuration;
using Game.Blazor.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Game.Blazor.Services
{
    public class PlayerHistoryService : IPlayerHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly TriviaApiOptions _triviaServerOptions;

        public PlayerHistoryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _triviaServerOptions = configuration.GetSection(nameof(TriviaApiOptions)).Get<TriviaApiOptions>();
        }

        public async Task SaveAsync(PlayerHistoryModel model)
        {
            var modelJson = new StringContent(JsonSerializer.Serialize(new { model.Player, model.Question, model.AnswerDate, model.AnsweredCorrectly, model.AnswerTime }), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_triviaServerOptions.BaseUrl}/PlayerHistory", modelJson);
            return;
        }
    }
}
