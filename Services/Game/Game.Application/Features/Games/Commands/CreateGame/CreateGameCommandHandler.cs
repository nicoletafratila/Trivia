using Common.ViewModels;
using Game.Application.Factories;
using Game.Application.Interfaces.Persistence;
using MediatR;

namespace Game.Application.Features.Games.Commands.CreateGame;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, CreateGameCommandResponse>
{
    private readonly Dictionary<GameType, TriviaGameFactory> _gameFactories;

    public CreateGameCommandHandler(ITriviaGamesRepository gameRepository, IQuestionsRepository questionsRepository)
    {
        _gameFactories = new Dictionary<GameType, TriviaGameFactory>
        {
            { GameType.Finite,  new FiniteGameFactory(gameRepository, questionsRepository) },
            { GameType.Infinite,  new InfiniteGameFactory(gameRepository, questionsRepository) }
        };
    }

    public async Task<CreateGameCommandResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            var newGame = await _gameFactories[request.GameType].Create(request.Name, request.CategoryId);
            return new CreateGameCommandResponse { GameId = newGame.Id };
        }
        return new CreateGameCommandResponse();
    }
}