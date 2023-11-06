using Game.Domain.Common;

namespace Game.Domain.Entities;

public class GameParticipant : Entity<long>, IAggregateRoot
{
    public string? UserId { get; private set; }

    public long PlayerId { get; private set; }

    public long TriviaGameId { get; private set; }

    public string? Email { get; private set; }

    public bool IsOnline { get; set; }

    //total score for a given game
    public long Score { get; set; }

    //will use this to check if participant has alrd answered current question in repeated case submission.
    public long LastAnsweredGameQuestionId { get; set; }

    public GameParticipant(string userId)
    {
        UserId = userId;
    }
}