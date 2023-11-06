using Common.ViewModels;
using MediatR;

namespace Game.Application.Features.Games.Commands.CreateGame;

public class CreateGameCommand : IRequest<CreateGameCommandResponse>
{
    public string? Name { get; set; }
    public GameType GameType { get; set; }
    public int CategoryId { get; set; }
}