using Game.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game.Domain.Entities;

public class Question : Entity<long>, IAggregateRoot
{
    public string? Text { get; private set; }

    private readonly List<Answer> _answers = new();
    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();
    public Guid CorrectAnswerId { get; private set; }
    public int CategoryId { get; private set; }

    [ForeignKey("CategoryId")] 
    public Category? Category { get; private set; }

    public Question WithText(string? questionText)
    {
        if (string.IsNullOrEmpty(questionText)) throw new ArgumentNullException(nameof(questionText));
        Text = questionText;

        return this;
    }

    public Question WithIncorrectAnswers(IEnumerable<string>? incorrectAnswers)
    {
        if (incorrectAnswers != null && incorrectAnswers.Any())
        {
            foreach (var incorrectAnswer in incorrectAnswers)
            {
                _answers.Add(new Answer(Guid.NewGuid(), incorrectAnswer));
            }
        }

        return this;
    }

    public Question WithCorrectAnswer(string? questionText)
    {
        var corrAnswer = new Answer(Guid.NewGuid(), questionText);
        _answers.Add(corrAnswer);
        CorrectAnswerId = corrAnswer.Id;
        return this;
    }

    public Question WithCategory(int categoryId)
    {
        if (categoryId == 0) throw new ArgumentNullException(nameof(categoryId));
        CategoryId = categoryId;
        return this;
    }
}