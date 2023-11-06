using Common.ViewModels;
using MediatR;

namespace Game.Application.Features.Players.Commands.UpdatePerformance
{
    public class UpdatePerformanceCommand : IRequest<PlayerModel>
    {
        public string UserId { get; set; } = "";
        public bool AnsweredCorrectly { get; set; }
        public TimeSpan AnswerTime { get; set; }
    }
}
