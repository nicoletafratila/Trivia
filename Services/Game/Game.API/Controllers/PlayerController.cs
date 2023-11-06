using Game.Application.Features.Players.Commands.AddCommand;
using Game.Application.Features.Players.Commands.UpdatePerformance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TriviaCsv.Services.Game.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ISender _mediator;

        public PlayerController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<AddPlayerCommandResponse> PostAsync(AddPlayerCommand addPlayerCommand)
        {
            return await _mediator.Send(addPlayerCommand);
        }

        [HttpPut]
        public async Task UpdatePerformance(UpdatePerformanceCommand updatePerformanceCommand)
        {
             await _mediator.Send(updatePerformanceCommand);
        }
    }
}
