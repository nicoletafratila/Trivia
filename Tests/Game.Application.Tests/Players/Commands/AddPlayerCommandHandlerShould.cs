using AutoFixture.Xunit2;
using FluentAssertions;
using Game.Application.Features.Players.Commands.AddCommand;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using Xunit;

namespace Game.Application.Tests.Players.Commands
{
    public class AddPlayerCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task CreateANewPlayerWhenSuccessful(
            AddPlayerCommand command,
            [Frozen] Mock<IPlayersRepository> playersRepositoryMock,
            AddPlayerCommandHandler sut)
        {
            //Arrange
            var player = new Player(Guid.NewGuid().ToString(), "testNickName");
            playersRepositoryMock.Setup(call => call.AddAsync(It.IsAny<Player>())).ReturnsAsync(player);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeOfType<AddPlayerCommandResponse>();
            playersRepositoryMock.VerifyAll();
        }
    }
}
