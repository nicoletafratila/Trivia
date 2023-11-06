using Microsoft.AspNetCore.SignalR;

namespace TriviaCsv.Services.Game.API.Hubs
{
    public class NewCurrentQuestionHub : Hub
    {
        public Task NewCurrentQuestion(float timeLeft)
        {
            return Task.CompletedTask;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("UserConnected");
            await base.OnConnectedAsync();
            
        }
    }
}
