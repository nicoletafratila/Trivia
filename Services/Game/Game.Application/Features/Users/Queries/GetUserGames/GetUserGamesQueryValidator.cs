using FluentValidation;

namespace Game.Application.Features.Users.Queries.GetUserGames
{
    public class GetUserGamesQueryValidator : AbstractValidator<GetUserGamesQuery>
    {
        public GetUserGamesQueryValidator()
        {
        }
    }
}
