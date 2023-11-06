using Common.ViewModels;
using MediatR;

namespace Game.Application.Features.Users.Queries.GetUserBestStreaks
{
    public class GetUserBestStreaksQuery : IRequest<IEnumerable<UserGameModel>>
    {
    }
}
