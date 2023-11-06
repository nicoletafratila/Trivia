using Game.Domain.Common;

namespace Game.Domain.Entities
{
    public class Player : Entity<long>, IAggregateRoot
    {
        public string? UserId { get; set; }
        public string? NickName { get; set; }
        public int CurrentStreak { get; set; }
        public int HighestStreak { get; set; }
        public TimeSpan FastestAnswerTime { get; set; }
        public int TotalPoint { get; set; }

        public Player(string userId, string? nickName, int currentStreak, int highestStreak, TimeSpan fastestAnswerTime, int totalPoint)
        {
            UserId = userId;
            NickName = nickName;
            CurrentStreak = currentStreak;
            HighestStreak = highestStreak;
            FastestAnswerTime = fastestAnswerTime;
            TotalPoint = totalPoint;
        }

        public Player(string? userId, string? nickName)
        {
            UserId = userId;
            NickName = nickName;
        }

        public void UpdatePerformance(bool answeredCorrectly, TimeSpan answerTime)
        {
            if (answeredCorrectly)
            {
                CurrentStreak++;
                TotalPoint += 10;

                if (FastestAnswerTime == TimeSpan.Zero)
                {
                    FastestAnswerTime = answerTime;
                }

                if (answerTime != TimeSpan.Zero && answerTime < FastestAnswerTime)
                    FastestAnswerTime = answerTime;

            }
            else CurrentStreak = 0;

            if (CurrentStreak > HighestStreak)
                HighestStreak = CurrentStreak;
            
        }
    }
}
