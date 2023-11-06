using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using MediatR;

namespace Game.Application.Features.PlayersHistory.Commands.AddPlayerHistory
{
    public class AddPlayerHistoryCommandHandler : IRequestHandler<AddPlayerHistoryCommand, AddPlayerHistoryCommandResponse>
    {
        private readonly IPlayersHistoryRepository _playersHistoryRepository;
        private readonly IPlayersRepository _playersRepository;
        private readonly IQuestionsRepository _questionsRepository;

        public AddPlayerHistoryCommandHandler(IPlayersHistoryRepository playersHistoryRepository, IPlayersRepository playersRepository, IQuestionsRepository questionsRepository)
        {
            _playersHistoryRepository = playersHistoryRepository;
            _playersRepository = playersRepository;
            _questionsRepository = questionsRepository;
        }

        public async Task<AddPlayerHistoryCommandResponse> Handle(AddPlayerHistoryCommand request, CancellationToken cancellationToken)
        {
            var player = await _playersRepository.GetPlayerFromDB(request.Player!);
            var question = await _questionsRepository.GetQuestionByIdIncludeAnswersAsync(request.Question);
            var playerHistory = new PlayerHistory(request.AnswerDate,player, question!, request.AnsweredCorrectly,request.AnswerTime);
            playerHistory = await _playersHistoryRepository.AddAsync(playerHistory);
            return new AddPlayerHistoryCommandResponse { PlayerHistoryId = playerHistory.Id };
        }
    
    }
}
