using Game.Application.Features.PlayersHistory.Commands.AddPlayerHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TriviaCsv.Services.Game.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerHistoryController : ControllerBase
    {
        private readonly ISender _mediator;

        public PlayerHistoryController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<AddPlayerHistoryCommandResponse> PostAsync(AddPlayerHistoryCommand addPlayerHistoryCommand)
        {
            return await _mediator.Send(addPlayerHistoryCommand);
        }
    }
}
