using AutoMapper;
using Common.ViewModels;
using Game.Application.Interfaces.Persistence;
using MediatR;

namespace Game.Application.Features.Games.Queries.GetGameCategories;

public class GetGameCategoriesQueryHandler : IRequestHandler<GetGameCategoriesQuery, IEnumerable<CategoryModel>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetGameCategoriesQueryHandler(ICategoryRepository categoriesRepository, IMapper mapper)
    {
        _categoryRepository = categoriesRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryModel>> Handle(GetGameCategoriesQuery request, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.ListAllAsync();
        return _mapper.Map<IEnumerable<CategoryModel>>(results).OrderBy(r => r.Name);
    }
}