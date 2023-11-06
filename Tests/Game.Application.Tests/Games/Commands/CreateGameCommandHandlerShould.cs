using AutoFixture.Xunit2;
using Common.ViewModels;
using FluentAssertions;
using Game.Application.Features.Games.Commands.CreateGame;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using Xunit;

namespace Game.Application.Tests.Games.Commands
{
    public class CreateGameCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task CreateANewFiniteGameWhenSuccessful(
            CreateGameCommand command,
            TriviaGame game,
            [Frozen] Mock<ITriviaGamesRepository> triviaGameRepositoryMock,
            [Frozen] Mock<IQuestionsRepository> questionsRepositoryMock,
            CreateGameCommandHandler sut)
        {
            var questions = new List<Question>
            {
                new Question().WithText("q1"),
                new Question().WithText("q2"),
                new Question().WithText("q3"),
                new Question().WithText("q4"),
                new Question().WithText("q5"),
                new Question().WithText("q6"),
                new Question().WithText("q7"),
                new Question().WithText("q8"),
                new Question().WithText("q9"),
                new Question().WithText("q10")
            };

            //Arrange
            questionsRepositoryMock.Setup(call => call.GetQuestionsForOneCategoryIncludeAnswersAsync(1)).ReturnsAsync(questions);
            triviaGameRepositoryMock.Setup(call => call.AddAsync(It.IsAny<TriviaGame>())).ReturnsAsync(game);

            //Act
            command.Name = "Finit game";
            command.GameType = GameType.Finite;
            command.CategoryId = 1;
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeOfType<CreateGameCommandResponse>();
            result.GameId.Should().Be(game.Id);
            questionsRepositoryMock.VerifyAll();
            triviaGameRepositoryMock.VerifyAll();
        }

        [Theory]
        [AutoMoqData]
        public async Task CreateANewIniniteGameWhenSuccessful(
            CreateGameCommand command,
            TriviaGame game,
            [Frozen] Mock<ITriviaGamesRepository> triviaGameRepositoryMock,
            [Frozen] Mock<IQuestionsRepository> questionsRepositoryMock,
            CreateGameCommandHandler sut)
        {
            var questions = new List<Question>
            {
                new Question().WithText("q1"),
                new Question().WithText("q2"),
                new Question().WithText("q3"),
                new Question().WithText("q4"),
                new Question().WithText("q5"),
                new Question().WithText("q6"),
                new Question().WithText("q7"),
                new Question().WithText("q8"),
                new Question().WithText("q9"),
                new Question().WithText("q10")
            };

            //Arrange
            questionsRepositoryMock.Setup(call => call.GetQuestionsForOneCategoryIncludeAnswersAsync(1)).ReturnsAsync(questions);
            triviaGameRepositoryMock.Setup(call => call.AddAsync(It.IsAny<TriviaGame>())).ReturnsAsync(game);
            triviaGameRepositoryMock.Setup(call => call.UpdateAsync(It.IsAny<TriviaGame>())).Returns(Task.FromResult(game));

            //Act
            command.Name = "Ininit game";
            command.GameType = GameType.Infinite;
            command.CategoryId = 1;
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeOfType<CreateGameCommandResponse>();
            result.GameId.Should().Be(game.Id);
            questionsRepositoryMock.VerifyAll();
            triviaGameRepositoryMock.VerifyAll();
        }
    }
}
