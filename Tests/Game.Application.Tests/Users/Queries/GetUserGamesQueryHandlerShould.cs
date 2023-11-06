using AutoFixture.Xunit2;
using Common.ViewModels;
using FluentAssertions;
using Game.Application.Features.Users.Queries.GetUserGames;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Game.Application.Tests.Users.Queries
{
    public class GetUserGamesQueryHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnAllQuestionsWhenSuccessful(
            GetUserGamesQuery query,
            ApplicationUserModel user,
            Player player,
            [Frozen] Mock<TriviaGame> gameMock,
            [Frozen] Mock<ITriviaGamesRepository> triviaGamesRepositoryMock,
            [Frozen] Mock<IPlayersRepository> playersRepositoryMock)
        {
            //Arrange
            gameMock.SetupGet(x => x.GameParticipants).Returns(new List<GameParticipant>() { new GameParticipant("user1") });
            gameMock.Object.Id = 1;
            gameMock.Object.Name = "game1";
            playersRepositoryMock.Setup(x => x.GetPlayerFromDB(It.IsAny<string>())).ReturnsAsync(player);

            GetUserGamesQueryHandler sut = new GetUserGamesQueryHandler(triviaGamesRepositoryMock.Object, playersRepositoryMock.Object);
            triviaGamesRepositoryMock.Setup(call => call.ListAllGamesIncludeParticipants()).ReturnsAsync(new List<TriviaGame>() { gameMock.Object });

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(user)),
            };
            
            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().HaveCount(1);
            triviaGamesRepositoryMock.VerifyAll();
            playersRepositoryMock.VerifyAll();
        }
    }
}
