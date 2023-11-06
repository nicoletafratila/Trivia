using EventBus.Contracts.Events;
using Game.Application.Features.Players.Commands.AddCommand;
using MassTransit;
using MediatR;
using TriviaCsv.Services.Game.API.Controllers;

namespace TriviaCsv.Services.Game.API.EventBusConsumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        private PlayerController? _playerController;

        public UserRegisteredConsumer(ISender mediator)
        {
            _playerController = new PlayerController(mediator);
        }

        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            if (_playerController != null)
            {
                var command = new AddPlayerCommand() { UserId = context.Message.UserId, NickName = context.Message.NickName };
                await _playerController.PostAsync(command);
            }
        }
    }
}
