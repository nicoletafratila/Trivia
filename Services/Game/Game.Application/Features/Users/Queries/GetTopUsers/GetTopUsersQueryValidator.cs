using FluentValidation;

namespace Game.Application.Features.Users.Queries.GetTopUsers
{
    public class GetTopUserGamesQueryValidator : AbstractValidator<GetTopUsersQuery>
    {
        public GetTopUserGamesQueryValidator()
        {
            RuleFor(x => x.QueryParameters).NotNull();
        }
    }
}
