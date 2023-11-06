using Common.ViewModels;
using MediatR;

namespace Game.Application.Features.Games.Queries.GetGameCategories
{
    public class GetGameCategoriesQuery : IRequest<IEnumerable<CategoryModel>>
    {
    }
}
