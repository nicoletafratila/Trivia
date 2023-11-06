using Common.ViewModels;
using Common.ViewModels.Pagination;
using Game.Application.Features.Users.Queries.GetTopUsers;
using Game.Application.Features.Users.Queries.GetUserBestStreaks;
using Game.Application.Features.Users.Queries.GetUserGames;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TriviaCsv.Services.Game.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly ISender _mediator;

        public HistoryController(ISender mediator, IConfiguration configuration)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the top users by score filtered by the parameters.
        /// </summary>
        /// <param name="queryStringParameters">Query parameters to filter data.</param>
        /// <returns>Paginated list of users.</returns>
        [HttpGet("topusers")]
        public async Task<PagedList<UserGameModel>> GetTopUsersAsync([FromQuery] QueryParameters queryStringParameters)
        {
            GetTopUsersQuery query = new()
            {
                QueryParameters = queryStringParameters
            };

            return await _mediator.Send(query);
        }

        [HttpGet("usergames")]
        public async Task<IEnumerable<UserGameModel>> GetUserGamesAsync()
        {
            GetUserGamesQuery query = new GetUserGamesQuery();
            return await _mediator.Send(query);
        }

        [HttpGet("userbeststreaks")]
        public async Task<IEnumerable<UserGameModel>> GetUserBestStreaksAsync()
        {
            GetUserBestStreaksQuery query = new GetUserBestStreaksQuery();
            return await _mediator.Send(query);
        }
    }
}
