using Game.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.SignalR;
using TriviaCsv.Services.Game.API.Hubs;

namespace TriviaCsv.Services.Game.API.Services
{
    public class QuestionTimerService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory scopeFactory;
        private Timer? questionTimer;
        private IHubContext<NewCurrentQuestionHub> _hubContext;
        private float _timeLeft = 15.0f;

        public QuestionTimerService(IServiceScopeFactory scopeFactory, IHubContext<NewCurrentQuestionHub> hubContext)
        {
            _hubContext = hubContext;
            this.scopeFactory = scopeFactory;            
        }

        public void Dispose()
        {
            questionTimer!.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            questionTimer = new Timer(GetNewQuestion, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(_timeLeft));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void GetNewQuestion(object? statusInfo)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _triviaGameRep = scope.ServiceProvider.GetRequiredService<ITriviaGamesRepository>();

                var allGames = await _triviaGameRep.GetAllGames();

                foreach (var game in allGames)
                {
                    var gameEntity = await _triviaGameRep.GetGameByIdIncludeCurrentAndNextQuestion(game.Id);
                    gameEntity!.AdvanceToNextQuestion();
                    await _triviaGameRep.UpdateAsync(gameEntity);                    
                }
            }
            await _hubContext.Clients.All.SendAsync("NewCurrentQuestion", _timeLeft);
        }
    }
}
