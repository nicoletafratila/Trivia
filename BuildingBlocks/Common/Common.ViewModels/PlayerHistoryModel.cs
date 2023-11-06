namespace Common.ViewModels
{
    public class PlayerHistoryModel
    {
        public DateTime AnswerDate { get; set; }
        public string? Player { get; set; }
        public long Question { get; set; }
        public bool AnsweredCorrectly { get; set; }
        public TimeSpan AnswerTime { get; set; }
    }
}
