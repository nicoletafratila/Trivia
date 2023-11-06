namespace Game.Blazor.Services.Interfaces
{
    public interface IPlayerService
    {
        Task UpdatePerformance(string UserID, bool AnsweredCorrectly, TimeSpan AnswerTime);
    }
}
