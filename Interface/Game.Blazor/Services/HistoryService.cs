using Common.ViewModels;
using Common.ViewModels.Pagination;
using Game.Blazor.Configuration;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace Game.Blazor.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly TriviaApiOptions _triviaApiOptions;

        public HistoryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _triviaApiOptions = configuration.GetSection(nameof(TriviaApiOptions)).Get<TriviaApiOptions>();
        }

        public async Task<IEnumerable<UserGameModel>?> GetUserGamesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserGameModel>?>($"{_triviaApiOptions.BaseUrl}/history/usergames");
        }

        public async Task<PagedList<UserGameModel>?> GetUsersAsync(QueryParameters queryStringParameters)
        {
            var query = new Dictionary<string, string>
            {
                [nameof(QueryParameters.PageSize)] = queryStringParameters.PageSize.ToString(),
                [nameof(QueryParameters.PageNumber)] = queryStringParameters.PageNumber.ToString()
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"{_triviaApiOptions.BaseUrl}/history/topusers", query));
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PagedList<UserGameModel>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<UserGameModel>?> GetUserBestStreaksAsync()
        {
           return await _httpClient.GetFromJsonAsync<IEnumerable<UserGameModel>?>($"{_triviaApiOptions.BaseUrl}/history/userbeststreaks");
        }
    }
}
