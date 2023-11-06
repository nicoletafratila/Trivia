using Common.ViewModels;
using Game.Application.Features.Games.Commands.CreateGame;
using Game.Application.Features.Games.Queries.GetCurrentQuestion;
using Game.Application.Features.Games.Queries.GetGames;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TriviaCsv.Services.Game.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TriviaGameController: ControllerBase
	{
		private readonly IMediator _mediator;

		public TriviaGameController(IMediator mediator)
		{
			_mediator = mediator;
		}

        [HttpGet]
        public async Task<IEnumerable<TriviaGameModel>> GetAllAsync()
        {
            return await _mediator.Send(new GetGamesQuery());
        }

        [HttpPost("")]
		public async Task<CreateGameCommandResponse> CreateGameAsync(CreateGameCommand createGameCommand)
		{
			return await _mediator.Send(createGameCommand);
		}

        [HttpGet("{id:long}/currentQuestion")]
		public async Task<QuestionModel> GetQuestionAsync(long id)
		{
			return await _mediator.Send(new GetCurrentQuestionQuery{ GameId = id});
		}
	}
}