using Common.ViewModels;
using Common.ViewModels.Pagination;

namespace Game.Blazor.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<PagedList<QuestionModel>?> GetAllAsync(QueryParameters queryStringParameters);
        Task SaveAsync(QuestionModel model);
        Task<string?> DeleteAsync(long id);
        Task<QuestionModel?> GetCurrentQuestion(long id);
    }
}
