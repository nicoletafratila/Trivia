using FluentValidation;

namespace Game.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
    {
        public DeleteQuestionCommandValidator()
        {
            RuleFor(x => x.QuestionId).NotNull();
        }
    }
}
