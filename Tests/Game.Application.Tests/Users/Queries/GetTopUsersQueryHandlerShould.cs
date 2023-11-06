using AutoFixture.Xunit2;
using Common.ViewModels;
using FluentAssertions;
using Game.Application.Features.Users.Queries.GetTopUsers;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Game.Application.Tests.Users.Queries
{
    public class GetTopUsersQueryHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnTopUsersWhenSuccessful(
            GetTopUsersQuery query,
            ApplicationUserModel user,
            Player player,
            Mock<TriviaGame> gameMock,
            [Frozen] Mock<ITriviaGamesRepository> triviaGamesRepositoryMock,
            [Frozen] Mock<IPlayersRepository> playersRepositoryMock)
        {
            //Arrange
            gameMock.SetupGet(x => x.GameParticipants).Returns(new List<GameParticipant> { new("user1") });
            var games = new List<TriviaGame>();
            gameMock.Object.Id = 1;
            gameMock.Object.Name = "game1";
            games.Add(gameMock.Object);
            query.QueryParameters!.PageNumber = 1;

            playersRepositoryMock.Setup(x => x.GetPlayerFromDB(It.IsAny<string>())).ReturnsAsync(player);
            var sut = new GetTopUsersQueryHandler(triviaGamesRepositoryMock.Object, playersRepositoryMock.Object);
            triviaGamesRepositoryMock.Setup(call => call.ListAllGamesIncludeParticipants()).ReturnsAsync(games);

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(user)),
            };

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result!.Items.Should().HaveCount(1);
            triviaGamesRepositoryMock.VerifyAll();
            playersRepositoryMock.VerifyAll();
        }
    }
}
