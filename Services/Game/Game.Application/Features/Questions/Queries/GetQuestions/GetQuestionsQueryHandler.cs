using AutoMapper;
using Common.ViewModels;
using Common.ViewModels.Extensions;
using Game.Application.Interfaces.Persistence;
using MediatR;

namespace Game.Application.Features.Questions.Queries.GetQuestions;

public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, PagedList<QuestionModel>>
{
    private readonly IQuestionsRepository _questionRepository;
    private readonly IMapper _mapper;

    public GetQuestionsQueryHandler(IQuestionsRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<QuestionModel>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetQuestionsIncludeAnswersAsync();
        var results = _mapper.Map<List<QuestionModel>>(questions).OrderBy(r => r.CategoryName).ThenBy(r => r.Text);
        return results.ToPagedList(request.QueryParameters!.PageNumber, request.QueryParameters.PageSize);
    }
}
