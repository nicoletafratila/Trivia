using AutoFixture.Xunit2;
using FluentAssertions;
using Game.Application.Features.Games.Queries.GetCurrentQuestion;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using Xunit;

namespace Game.Application.Tests.Games.Queries
{
    public class GetCurrentQuestionQueryHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnCurrentQuestionWhenSuccessful(
            GetCurrentQuestionQuery query,
            [Frozen] Mock<ITriviaGamesRepository> triviaGameRepositoryMock,
            [Frozen] Mock<IQuestionsRepository> questionsRepositoryMock,
            GetCurrentQuestionQueryHandler sut)
        {

            var question = new Question()
                .WithText("q1")
                .WithCategory(1)
                .WithIncorrectAnswers(new List<string> { "a2", "a3", "a4" })
                .WithCorrectAnswer("a1");

            var game = new Mock<TriviaGame>().Object;
            game.CurrentGameQuestion = new GameQuestion(1,new Guid());
            triviaGameRepositoryMock.Setup(call => call.GetGameByIdIncludeCurrentAndNextQuestion(query.GameId)).ReturnsAsync(game);
            questionsRepositoryMock.Setup(call => call.GetQuestionByIdIncludeAnswersAsync(game.CurrentGameQuestion.QuestionId)).ReturnsAsync(question);
            
            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            triviaGameRepositoryMock.VerifyAll();
            questionsRepositoryMock.VerifyAll();        }
    }
}
