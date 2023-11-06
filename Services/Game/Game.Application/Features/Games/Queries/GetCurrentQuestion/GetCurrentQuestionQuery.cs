using Common.ViewModels;
using MediatR;

namespace Game.Application.Features.Games.Queries.GetCurrentQuestion;

public class GetCurrentQuestionQuery : IRequest<QuestionModel>
{
    public long GameId { get; set; }
}