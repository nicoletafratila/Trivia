using MediatR;

namespace Game.Application.Features.Players.Commands.AddCommand
{
    public class AddPlayerCommand : IRequest<AddPlayerCommandResponse>
    {
        public string? UserId { get; set; }
        public string? NickName { get; set; }
    }
}
