using AutoFixture.Xunit2;
using AutoMapper;
using Common.ViewModels;
using FluentAssertions;
using Game.Application.Features.Games.Queries.GetGames;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using Xunit;

namespace Game.Application.Tests.Games.Queries
{
    public class GetGamesQueryHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnAllGamesWhenSuccessful(
            GetGamesQuery query,
            [Frozen] Mock<ITriviaGamesRepository> triviaGamesRepositoryMock,
            [Frozen] Mock<IMapper> triviaGameMapperMock,
            GetGamesQueryHandler sut)
        {

            //Arrange
            var games = new List<TriviaGame>()
            {
                new Mock<TriviaGame>().Object,
            };
            triviaGamesRepositoryMock.Setup(call => call.ListAllAsync()).ReturnsAsync(games);
            triviaGameMapperMock.Setup(x => x.Map<IEnumerable<TriviaGameModel>>(It.IsAny<IReadOnlyList<TriviaGame>>()))
            .Returns((IReadOnlyList<TriviaGame> source) =>
            {
                return source.Select(item => new TriviaGameModel() { Name = item.Name });
            });

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().HaveCount(1);
            triviaGamesRepositoryMock.VerifyAll();
            triviaGameMapperMock.VerifyAll();
        }
    }
}
