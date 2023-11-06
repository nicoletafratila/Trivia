using Common.ViewModels;
using MediatR;

namespace Game.Application.Features.Users.Queries.GetUserGames
{
    public class GetUserGamesQuery : IRequest<IEnumerable<UserGameModel>>
    {
    }
}
