using AutoMapper;
using Common.ViewModels;
using Game.Application.Interfaces.Persistence;
using MediatR;

namespace Game.Application.Features.Games.Queries.GetGames
{
    public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<TriviaGameModel>>
    {
        private readonly ITriviaGamesRepository _gamesRepository;
        private readonly IMapper _mapper;

        public GetGamesQueryHandler(ITriviaGamesRepository gamesRepository, IMapper mapper)
        {
            _gamesRepository = gamesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TriviaGameModel>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            var results = await _gamesRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<TriviaGameModel>>(results).OrderBy(r => r.Name);
        }
    }
}
