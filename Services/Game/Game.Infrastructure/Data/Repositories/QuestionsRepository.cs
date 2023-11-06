using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Data.Repositories;

public class QuestionsRepository: BaseRepository<Question,long>, IQuestionsRepository
{
    public QuestionsRepository(TriviaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Question>> GetQuestionsIncludeAnswersAsync()
    {
        return await DbContext.Questions
                        .Include(a => a.Category)
                        .Include(a => a.Answers).ToListAsync();
    }

    public async Task<Question?> GetQuestionByIdIncludeAnswersAsync(long id)
    {
        return await DbContext.Questions.Include(a => a.Answers).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Question>> GetQuestionsForOneCategoryIncludeAnswersAsync(int categoryId)
    {
        return await DbContext.Questions
                        .Where(a => a.CategoryId == categoryId)
                        .Include(a => a.Category)
                        .Include(a => a.Answers).ToListAsync();
    }
}