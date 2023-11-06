using FluentValidation;

namespace Game.Application.Features.Users.Queries.GetUserBestStreaks
{
    public class GetUserGamesQueryValidator : AbstractValidator<GetUserBestStreaksQuery>
    {
        public GetUserGamesQueryValidator()
        {
        }
    }
}
