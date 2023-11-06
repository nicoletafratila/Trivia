using Common.ViewModels;
using Common.ViewModels.Extensions;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using MediatR;

namespace Game.Application.Features.Users.Queries.GetTopUsers
{
    public class GetTopUsersQueryHandler : IRequestHandler<GetTopUsersQuery, PagedList<UserGameModel>>
    {
        private readonly ITriviaGamesRepository _gamesRepository;
        private readonly IPlayersRepository _playersRepository;

        public GetTopUsersQueryHandler(ITriviaGamesRepository gamesRepository, IPlayersRepository playersRepository)
        {
            _gamesRepository = gamesRepository;
            _playersRepository = playersRepository; 
        }

        public async Task<PagedList<UserGameModel>> Handle(GetTopUsersQuery request, CancellationToken cancellationToken)
        {
            var games = await _gamesRepository.ListAllGamesIncludeParticipants();
            IList<UserGameModel> result = new List<UserGameModel>();
            foreach (var game in games)
            {
                if (game.GameParticipants != null)
                {
                    foreach (var participant in game.GameParticipants)
                    {
                        var player = await _playersRepository.GetPlayerFromDB(participant.UserId!);
                        if (player != null)
                        {
                            var model = MapToGameModel(player, participant, game);
                            result.Add(model);
                        }
                    }
                }
            }

            result = GetTopUsersByScore(result);
            return result.ToPagedList(request.QueryParameters!.PageNumber, request.QueryParameters.PageSize);
        }

        private static UserGameModel MapToGameModel(Player user, GameParticipant participant, TriviaGame game)
        {
            UserGameModel model = new()
            {
                UserId = user.UserId,
                NickName = user.NickName,
                Score = participant.Score,
                GameName = game.Name
            };

            return model;
        }

        private static IList<UserGameModel> GetTopUsersByScore(IEnumerable<UserGameModel> users)
        {
            return users.GroupBy(item => item.UserId)
                .Select(item => new UserGameModel
                {
                    UserId = item.First().UserId,
                    NickName = item.First().NickName,
                    GameName = item.First().GameName,
                    Score = item.Max(q => q.Score)
                })
                .OrderByDescending(item => item.Score)
                .ToList();
        }
    }
}
