namespace Common.ViewModels
{
    public class PlayerModel
    {
        public string? UserId { get; set; }
        public string? NickName { get; set; }
        public int CurrentStreak { get; set; }
        public int HighestStreak { get; set; }
        public TimeSpan FastestAnswerTime { get; set; }
        public int TotalPoint { get; set; }
    }
}
