using FluentValidation;

namespace Game.Application.Features.Questions.Commands.AddQuestion
{
    public class AddQuestionCommandValidator : AbstractValidator<AddQuestionCommand>
    {
        public AddQuestionCommandValidator()
        {
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.CorrectAnswer).NotEmpty();
            RuleFor(x => x.IncorrectAnswers).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
