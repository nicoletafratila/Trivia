using AutoMapper;
using Common.ViewModels;
using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using MediatR;

namespace Game.Application.Features.Games.Queries.GetCurrentQuestion;

public class GetCurrentQuestionQueryHandler : IRequestHandler<GetCurrentQuestionQuery, QuestionModel>
{
    private readonly ITriviaGamesRepository _gamesRepository;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IMapper _mapper;

    public GetCurrentQuestionQueryHandler(ITriviaGamesRepository gamesRepository, IQuestionsRepository questionsRepository, IMapper mapper)
    {
        _gamesRepository = gamesRepository;
        _questionsRepository = questionsRepository;
        _mapper = mapper;
    }

    public async Task<QuestionModel> Handle(GetCurrentQuestionQuery request, CancellationToken cancellationToken)
    {
        var game = await _gamesRepository.GetGameByIdIncludeCurrentAndNextQuestion(request.GameId);
        var question = new Question();
        if (game != null && game.CurrentGameQuestion != null)
        {
            question = await _questionsRepository.GetQuestionByIdIncludeAnswersAsync(game.CurrentGameQuestion.QuestionId);
        }
        return _mapper.Map<QuestionModel>(question);
    }
}