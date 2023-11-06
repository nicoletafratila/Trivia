using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using MediatR;

namespace Game.Application.Features.Players.Commands.AddCommand
{
    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, AddPlayerCommandResponse>
    {
        private readonly IPlayersRepository _playerRepository;

        public AddPlayerCommandHandler(IPlayersRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<AddPlayerCommandResponse> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
        {
            var player = new Player(request.UserId, request.NickName);
            player = await _playerRepository.AddAsync(player);
            return new AddPlayerCommandResponse { PlayerId = player.Id };
        }
    }
}
