using Game.Domain.Entities;

namespace Game.Application.Interfaces.Persistence;

public interface IQuestionsRepository: IAsyncRepository<Question,long>
{
    //the project can be configured to load all related entities by default.
    Task<IEnumerable<Question>> GetQuestionsIncludeAnswersAsync();
    Task<Question?> GetQuestionByIdIncludeAnswersAsync(long id);
    Task<IEnumerable<Question>> GetQuestionsForOneCategoryIncludeAnswersAsync(int categoryId);
    
}