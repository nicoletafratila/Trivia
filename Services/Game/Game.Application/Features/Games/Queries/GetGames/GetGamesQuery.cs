using Common.ViewModels;
using MediatR;

namespace Game.Application.Features.Games.Queries.GetGames;

public class GetGamesQuery : IRequest<IEnumerable<TriviaGameModel>>
{
}