using FluentValidation;

namespace Game.Application.Features.Players.Commands.AddCommand
{
    public class AddPlayerCommandValidator : AbstractValidator<AddPlayerCommand>
    {
        public AddPlayerCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.NickName).NotEmpty();
        }
    }
}
