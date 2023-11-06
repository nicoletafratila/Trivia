using FluentValidation;

namespace Game.Application.Features.Games.Queries.GetCurrentQuestion
{
    public class GetCurrentQuestionQueryValidator : AbstractValidator<GetCurrentQuestionQuery>
    {
        public GetCurrentQuestionQueryValidator()
        {
            RuleFor(x => x.GameId).NotNull();
        }
    }
}
