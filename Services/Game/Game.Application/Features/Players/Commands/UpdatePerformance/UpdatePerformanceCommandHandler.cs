using AutoMapper;
using Common.ViewModels;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using MediatR;
using System.Security.Claims;

namespace Game.Application.Features.Players.Commands.UpdatePerformance
{
    public class UpdatePerformanceCommandHandler : IRequestHandler<UpdatePerformanceCommand, PlayerModel>
    {
        private readonly IPlayersRepository _playersRepository;

        private readonly IMapper _mapper;

        public UpdatePerformanceCommandHandler(IPlayersRepository playersRepository, IMapper mapper)
        {
            _playersRepository = playersRepository;
            _mapper = mapper;
        }

        public async Task<PlayerModel> Handle(UpdatePerformanceCommand request, CancellationToken cancellationToken)
        {
            var player = await _playersRepository.UpdatePlayerPerformance(request.UserId, request.AnsweredCorrectly,request.AnswerTime);

            return _mapper.Map<PlayerModel>(player);
        }
    }
}