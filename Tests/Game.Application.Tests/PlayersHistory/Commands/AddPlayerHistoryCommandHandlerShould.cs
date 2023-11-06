using AutoFixture.Xunit2;
using FluentAssertions;
using Game.Application.Features.PlayersHistory.Commands.AddPlayerHistory;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using Xunit;

namespace Game.Application.Tests.PlayersHistory.Commands
{
    public class AddPlayerHistoryCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task CreateANewPlayerHistoryWhenSuccessful(
            AddPlayerHistoryCommand command,
            [Frozen] Mock<IPlayersHistoryRepository> playersHistoryRepositoryMock,
            AddPlayerHistoryCommandHandler sut)
        {
            //Arrange
            var playerHistory = new PlayerHistory(
                DateTime.UtcNow, 
                new Player(Guid.NewGuid().ToString(), "testNickName"),
                new Question(), true, TimeSpan.FromSeconds(5));

            playersHistoryRepositoryMock.Setup(call => call.AddAsync(It.IsAny<PlayerHistory>())).ReturnsAsync(playerHistory);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeOfType<AddPlayerHistoryCommandResponse>();
            playersHistoryRepositoryMock.VerifyAll();
        }
    }
}
