using Game.Domain.Entities;
using MediatR;

namespace Game.Application.Features.PlayersHistory.Commands.AddPlayerHistory
{
    public class AddPlayerHistoryCommand : IRequest<AddPlayerHistoryCommandResponse>
    {
        public DateTime AnswerDate { get; set; }
        public string? Player { get; set; }
        public long Question { get; set; }
        public bool AnsweredCorrectly { get; set; }
        public TimeSpan AnswerTime { get; set; }
    }
}
