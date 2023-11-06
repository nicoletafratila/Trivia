using AutoFixture.Xunit2;
using AutoMapper;
using Common.ViewModels;
using FluentAssertions;
using Game.Application.Features.Players.Commands.UpdatePerformance;
using Game.Application.Interfaces.Persistence;
using Game.Application.Mappers;
using Game.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Game.Application.Tests.Players.Commands
{
    public class UpdatePerformanceCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task IncrementPlayerScoreAndStreakWhenAnsweredCorrectly(
            UpdatePerformanceCommand command,
            [Frozen] Mock<IPlayersRepository> playersRepositoryMock,
            [Frozen] Mock<IMapper> playerMapperMock,
            UpdatePerformanceCommandHandler upcHandler)
        {
            //Arrange
            Player player = new Player("0", null, 0, 0, TimeSpan.Zero, 0);
            player.UpdatePerformance(true, TimeSpan.FromSeconds(1));
            playersRepositoryMock.Setup(call => 
                call.UpdatePlayerPerformance("0", true, It.IsAny<TimeSpan>())).ReturnsAsync(player);            
            command.AnsweredCorrectly = true;
            command.UserId = "0";
            playerMapperMock.Setup(x => x.Map<PlayerModel>(It.IsAny<Player>()))
            .Returns((Player source) =>
                new PlayerModel()
                {
                    CurrentStreak = source.CurrentStreak,
                    FastestAnswerTime = source.FastestAnswerTime,
                    HighestStreak = source.HighestStreak,
                    TotalPoint = source.TotalPoint
                });

            //Act
            var result = await upcHandler.Handle(command, CancellationToken.None);

            //Assert            
            result.Should().BeOfType<PlayerModel>();
            playersRepositoryMock.VerifyAll();
            playerMapperMock.VerifyAll();
            result.CurrentStreak.Should().Be(1);
            result.HighestStreak.Should().Be(1);
            result.FastestAnswerTime.Should().Be(TimeSpan.FromSeconds(1));
            result.TotalPoint.Should().Be(10);
        }

        [Theory]
        [AutoMoqData]
        public async Task ResetCurrentStreakWhenAnsweredIncorrectly(
            UpdatePerformanceCommand command,
            [Frozen] Mock<IPlayersRepository> playersRepositoryMock,
            [Frozen] Mock<IMapper> playerMapperMock,
            UpdatePerformanceCommandHandler upcHandler)
        {
            //Arrange
            Player player = new Player("0", null, 5, 5, TimeSpan.Zero, 10);
            player.UpdatePerformance(false, TimeSpan.FromSeconds(1));
            playersRepositoryMock.Setup(call =>
                call.UpdatePlayerPerformance("0", false, It.IsAny<TimeSpan>())).ReturnsAsync(player);
            command.AnsweredCorrectly = false;
            command.UserId = "0";
            playerMapperMock.Setup(x => x.Map<PlayerModel>(It.IsAny<Player>()))
            .Returns((Player source) =>
                new PlayerModel()
                {
                    CurrentStreak = source.CurrentStreak,
                    FastestAnswerTime = source.FastestAnswerTime,
                    HighestStreak = source.HighestStreak,
                    TotalPoint = source.TotalPoint
                });

            //Act
            var result = await upcHandler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeOfType<PlayerModel>();
            playerMapperMock.VerifyAll();
            playersRepositoryMock.VerifyAll();
            result.CurrentStreak.Should().Be(0);
            result.HighestStreak.Should().Be(5);
            result.FastestAnswerTime.Should().Be(TimeSpan.Zero);
            result.TotalPoint.Should().Be(10);
        }

    }
}
