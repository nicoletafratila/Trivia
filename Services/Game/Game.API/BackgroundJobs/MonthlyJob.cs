using EventBus.Contracts.Events;
using Game.Application.Interfaces.Persistence;
using MassTransit;
using Quartz;
using System.Text;

namespace TriviaCsv.Services.Game.API.BackgroundJobs
{
    public class MonthlyJob : IJob
    {
        private readonly ITriviaGamesRepository _triviaGamesRepository;
        private readonly IGameParticipantsRepository _gameParticipantsRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public MonthlyJob(ITriviaGamesRepository triviaGamesRepository, IGameParticipantsRepository gameParticipantsRepository, IPublishEndpoint publishEndpoint)
        {
            _triviaGamesRepository = triviaGamesRepository;
            _gameParticipantsRepository = gameParticipantsRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var triviaGames = await _triviaGamesRepository.ListAllAsync();
            var sb = new StringBuilder();
            
            foreach (var triviaGame in triviaGames)
            {
                var gameParticipants = await _gameParticipantsRepository.GetGameParticipantsRanksByTriviaGameId(triviaGame.Id);
                var emailsToSendTo = new List<string>();

                foreach (var gameParticipant in gameParticipants)
                {
                    sb.Append($"Username: {gameParticipant.UserId} Score: {gameParticipant.Score}\n");
                    if (!string.IsNullOrWhiteSpace(gameParticipant.Email))
                    {
                        emailsToSendTo.Add(gameParticipant.Email);
                    }
                }
                
                await _publishEndpoint.Publish<EmailMetadata>(new
                    {
                        To = emailsToSendTo,
                        Subject = "Your high scores",
                        Body = sb.ToString()
                    });

                sb.Clear();
                await _gameParticipantsRepository.ResetScoreForEachGameParticipantInTriviaGame(triviaGame.Id);
            }
        }
    }
}
