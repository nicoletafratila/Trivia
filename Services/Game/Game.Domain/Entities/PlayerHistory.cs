using Game.Domain.Common;

namespace Game.Domain.Entities
{
    public class PlayerHistory : Entity<long>, IAggregateRoot
    {
        public DateTime AnswerDate { get; set; }
        public Player? Player { get; set; }
        public Question? Question { get; set; }
        public bool AnsweredCorrectly { get; set; }
        public TimeSpan AnswerTime { get; set; }

        private PlayerHistory()
        {
        }

        public PlayerHistory(DateTime answerDate, Player player, Question question, bool answeredCorrectly, TimeSpan answerTime)
        {
            AnswerDate = answerDate;
            Player = player;
            Question = question;
            AnsweredCorrectly = answeredCorrectly;
            AnswerTime = answerTime;
        }
    }
}
