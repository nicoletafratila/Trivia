using AutoFixture.Xunit2;
using AutoMapper;
using Common.ViewModels;
using Common.ViewModels.Pagination;
using FluentAssertions;
using Game.Application.Features.Questions.Queries.GetQuestions;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using Xunit;

namespace Game.Application.Tests.Questions.Queries
{
    public class GetQuestionsQueryHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnAllQuestionsWhenSuccessful(
            GetQuestionsQuery query,
            List<Question> questions,
            [Frozen] Mock<IQuestionsRepository> questionsRepositoryMock,
            [Frozen] Mock<IMapper> questionMapperMock,
            GetQuestionsQueryHandler sut)
        {

            //Arrange
            questionsRepositoryMock.Setup(call => call.GetQuestionsIncludeAnswersAsync()).ReturnsAsync(questions);
            questionMapperMock.Setup(x => x.Map<List<QuestionModel>>(It.IsAny<List<Question>>()))
            .Returns((IEnumerable<Question> source) =>
            {
                return source.Select(item => new QuestionModel() { Text = item.Text }).ToList();
            });

            //Act
            query.QueryParameters = new QueryParameters() { PageNumber = 1, PageSize = 2 };
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result!.Items.Should().HaveCount(2);
            questionsRepositoryMock.VerifyAll();
            questionMapperMock.VerifyAll();
        }
    }
}
