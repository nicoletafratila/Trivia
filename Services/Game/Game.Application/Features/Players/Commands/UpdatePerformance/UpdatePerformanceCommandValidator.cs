using FluentValidation;

namespace Game.Application.Features.Players.Commands.UpdatePerformance
{
    public class UpdatePerformanceCommandValidator : AbstractValidator<UpdatePerformanceCommand>
    {
        public UpdatePerformanceCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.AnsweredCorrectly).NotNull();
            RuleFor(x => x.AnswerTime).NotNull();
        }
    }
}
