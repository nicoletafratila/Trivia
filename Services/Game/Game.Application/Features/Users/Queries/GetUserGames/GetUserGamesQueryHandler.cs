using Common.ViewModels;
using Game.Application.Interfaces.Persistence;
using MediatR;

namespace Game.Application.Features.Users.Queries.GetUserGames;

public class GetUserGamesQueryHandler : IRequestHandler<GetUserGamesQuery, IEnumerable<UserGameModel>>
{
    private readonly ITriviaGamesRepository _gamesRepository;
    private readonly IPlayersRepository _playersRepository;

    public GetUserGamesQueryHandler(ITriviaGamesRepository gamesRepository, IPlayersRepository playersRepository)
    {
        _gamesRepository = gamesRepository;
        _playersRepository = playersRepository;
    }

    public async Task<IEnumerable<UserGameModel>> Handle(GetUserGamesQuery request, CancellationToken cancellationToken)
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
                        UserGameModel model = new UserGameModel();
                        model.UserId = player.UserId;
                        model.NickName = player.NickName;
                        model.Score = participant.Score;
                        model.GameName = game.Name;
                        result.Add(model);
                    }
                }
            }
        }
        return result.OrderBy(model => model.NickName).ThenBy(model => model.GameName);
    }
}