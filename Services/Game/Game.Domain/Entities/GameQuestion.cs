using Game.Domain.Common;

namespace Game.Domain.Entities;
//a game should not own the questions, therefore we will use this entity as a way to separate the two
//also, by using this approach we can filter which questions are selected to be used 
public class GameQuestion : Entity<long>, IAggregateRoot
{
    public long QuestionId { get; private set; }

    public long? NextGameQuestionId { get; private set; }
    public virtual GameQuestion? NextGameQuestion { get; set; }

    public Guid CorrectAnswerId { get; private set; }

    private GameQuestion()
    {
    }

    public GameQuestion(long questionId, Guid correctAnswerId)
    {
        QuestionId = questionId;
        CorrectAnswerId = correctAnswerId;
    }
}