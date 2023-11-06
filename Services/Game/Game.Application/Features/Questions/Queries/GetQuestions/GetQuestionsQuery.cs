using Common.ViewModels;
using Common.ViewModels.Pagination;
using MediatR;

namespace Game.Application.Features.Questions.Queries.GetQuestions
{
    public class GetQuestionsQuery : IRequest<PagedList<QuestionModel>>
    {
        public QueryParameters? QueryParameters { get; set; }
    }
}
