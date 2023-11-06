using Common.ViewModels;
using Common.ViewModels.Pagination;
using MediatR;

namespace Game.Application.Features.Users.Queries.GetTopUsers
{
    public class GetTopUsersQuery : IRequest<PagedList<UserGameModel>>
    {
        public QueryParameters? QueryParameters { get; set; }
    }
}
