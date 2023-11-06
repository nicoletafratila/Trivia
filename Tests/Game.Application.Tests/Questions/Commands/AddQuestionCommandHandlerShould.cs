using AutoFixture.Xunit2;
using FluentAssertions;
using Game.Application.Features.Questions.Commands.AddQuestion;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using Xunit;

namespace Game.Application.Tests.Questions.Commands
{
    public class AddQuestionCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task CreateANewQuestionWhenSuccessful(
            AddQuestionCommand command,
            [Frozen] Mock<IQuestionsRepository> questionsRepositoryMock,
            AddQuestionCommandHandler sut)
        {
            //Arrange
            var question = new Question()
                .WithText("q1")
                .WithCategory(1)
                .WithIncorrectAnswers(new List<string> { "a2", "a3", "a4" })
                .WithCorrectAnswer("a1");
            questionsRepositoryMock.Setup(call => call.AddAsync(It.IsAny<Question>())).ReturnsAsync(question);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeOfType<AddQuestionCommandResponse>();
            questionsRepositoryMock.VerifyAll();
        }
    }
}
