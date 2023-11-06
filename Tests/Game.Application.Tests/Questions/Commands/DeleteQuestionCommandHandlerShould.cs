using AutoFixture.Xunit2;
using Game.Application.Features.Questions.Commands.DeleteQuestion;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Game.Application.Tests.Questions.Commands
{
    public class DeleteQuestionCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task DeleteTheQuestionWhenNotUsed(
            DeleteQuestionCommand command,
            Question question,
            List<GameQuestion> gameQuestions,
            [Frozen] Mock<IQuestionsRepository> questionsRepositoryMock,
            [Frozen] Mock<IGameQuestionsRepository> gameQuestionsRepositoryMock,
            DeleteQuestionCommandHandler sut)
        {
            //Arrange
            gameQuestionsRepositoryMock.Setup(call => call.ListAllAsync()).ReturnsAsync(gameQuestions);
            questionsRepositoryMock.Setup(call => call.GetQuestionByIdIncludeAnswersAsync(It.IsAny<long>())).ReturnsAsync(question);
            questionsRepositoryMock.Setup(call => call.DeleteAsync(It.IsAny<Question>())).Returns(Task.FromResult(new Unit()));

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            questionsRepositoryMock.VerifyAll();
            gameQuestionsRepositoryMock.VerifyAll();
        }

        [Theory]
        [AutoMoqData]
        public async Task ThrowExceptionBecauseTheQuestionWasNotFound(
            DeleteQuestionCommand command,
            List<GameQuestion> gameQuestions,
            [Frozen] Mock<IQuestionsRepository> questionsRepositoryMock,
            [Frozen] Mock<IGameQuestionsRepository> gameQuestionsRepositoryMock,
            DeleteQuestionCommandHandler sut)
        {
            //Arrange
            gameQuestionsRepositoryMock.Setup(call => call.ListAllAsync()).ReturnsAsync(gameQuestions);
            questionsRepositoryMock.Setup(call => call.GetQuestionByIdIncludeAnswersAsync(It.IsAny<long>())).ReturnsAsync(value: null);

            //Act
            var result = await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(command, CancellationToken.None));

            //Assert
            Assert.Equal("Could not find the question you want to delete.", result.Message);
            questionsRepositoryMock.VerifyAll();
            gameQuestionsRepositoryMock.VerifyAll();
        }

        [Theory]
        [AutoMoqData]
        public async Task ThrowExceptionBecauseTheQuestionIsUsed(
           DeleteQuestionCommand command,
           Question question,
           List<GameQuestion> gameQuestions,
           [Frozen] Mock<IQuestionsRepository> questionsRepositoryMock,
           [Frozen] Mock<IGameQuestionsRepository> gameQuestionsRepositoryMock,
           DeleteQuestionCommandHandler sut)
        {
            //Arrange
            gameQuestions[0] = new GameQuestion(question.Id, new Guid());
            gameQuestionsRepositoryMock.Setup(call => call.ListAllAsync()).ReturnsAsync(gameQuestions);
            questionsRepositoryMock.Setup(call => call.GetQuestionByIdIncludeAnswersAsync(It.IsAny<long>())).ReturnsAsync(question);

            //Act
            command.QuestionId = question.Id;
            var result = await Assert.ThrowsAsync<Exception>(async () => await sut.Handle(command, CancellationToken.None));

            //Assert
            Assert.Equal("The question  is already used in a game and can't be deleted.", result.Message);
            questionsRepositoryMock.VerifyAll();
            gameQuestionsRepositoryMock.VerifyAll();
        }
    }
}
