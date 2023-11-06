using AutoFixture.Xunit2;
using AutoMapper;
using Common.ViewModels;
using FluentAssertions;
using Game.Application.Features.Games.Queries.GetGameCategories;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Moq;
using Xunit;

namespace Game.Application.Tests.Games.Queries
{
    public class GetGameCategoriesQueryHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnAllCategoriesWhenSuccessful(
            GetGameCategoriesQuery query,
            [Frozen] Mock<ICategoryRepository> categoryRepositoryMock,
            [Frozen] Mock<IMapper> categoryMapperMock,
            GetGameCategoriesQueryHandler sut)
        {
            //Arrange
            var categories = new List<Category>()
            {
                new Category("Animal")
            };
            categoryRepositoryMock.Setup(call => call.ListAllAsync()).ReturnsAsync(categories);
            categoryMapperMock.Setup(x => x.Map<IEnumerable<CategoryModel>>(It.IsAny<IReadOnlyList<Category>>()))
            .Returns((IReadOnlyList<Category> source) =>
            {
                return source.Select(item => new CategoryModel() { Name = item.Name });
            });

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().HaveCount(1);
            categoryRepositoryMock.VerifyAll();
            categoryMapperMock.VerifyAll();
        }
    }
}
