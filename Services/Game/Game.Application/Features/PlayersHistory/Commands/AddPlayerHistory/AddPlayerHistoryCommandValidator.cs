using FluentValidation;

namespace Game.Application.Features.PlayersHistory.Commands.AddPlayerHistory
{
    public class AddPlayerHistoryCommandValidator : AbstractValidator<AddPlayerHistoryCommand>
    {
        public AddPlayerHistoryCommandValidator()
        {
            RuleFor(x => x.Player).NotNull();
            RuleFor(x => x.Question).NotNull();
            RuleFor(x => x.AnswerDate).NotNull();
            RuleFor(x => x.AnswerTime).NotNull();
            RuleFor(x => x.AnsweredCorrectly).NotNull();
        }
    }
}
