using FluentValidation;

namespace Game.Application.Features.Games.Commands.CreateGame
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.GameType).NotNull();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
