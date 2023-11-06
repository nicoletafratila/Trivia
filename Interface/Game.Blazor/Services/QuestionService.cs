using Common.ViewModels;
using Common.ViewModels.Pagination;
using Game.Blazor.Configuration;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;

namespace Game.Blazor.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly HttpClient _httpClient;
        private readonly TriviaApiOptions _triviaServerOptions;

        public QuestionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _triviaServerOptions = configuration.GetSection(nameof(TriviaApiOptions)).Get<TriviaApiOptions>();
        }

        public async Task<PagedList<QuestionModel>?> GetAllAsync(QueryParameters queryStringParameters)
        {
            var query = new Dictionary<string, string>
            {
                [nameof(QueryParameters.PageSize)] = queryStringParameters.PageSize.ToString(),
                [nameof(QueryParameters.PageNumber)] = queryStringParameters.PageNumber.ToString()
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"{_triviaServerOptions.BaseUrl}/Question", query));
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PagedList<QuestionModel>?>(await response.Content.ReadAsStringAsync());
        }

        public async Task SaveAsync(QuestionModel model)
        {
            var modelJson = new StringContent(JsonSerializer.Serialize(new { model.Text, model.CategoryId, model.CorrectAnswer, model.IncorrectAnswers }), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_triviaServerOptions.BaseUrl}/Question", modelJson);
            return;
        }

        public async Task<string?> DeleteAsync(long id)
        {
            using var response = await _httpClient.DeleteAsync($"{_triviaServerOptions.BaseUrl}/Question/{id}");
            var result = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new
            {
                Message = string.Empty
            })!;
            return result.Message;
        }

        public async Task<QuestionModel?> GetCurrentQuestion(long id)
        {
            return await JsonSerializer.DeserializeAsync<QuestionModel?>
                    (await _httpClient.GetStreamAsync($"{_triviaServerOptions.BaseUrl}/triviagame/{id}/currentQuestion"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
