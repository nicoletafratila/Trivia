using MediatR;

namespace Game.Application.Features.Questions.Commands.AddQuestion;

public class AddQuestionCommand : IRequest<AddQuestionCommandResponse>
{
    public string? Text { get; set; }
    public IEnumerable<string>? IncorrectAnswers { get; set; }
    public string? CorrectAnswer { get; set; }
    public int CategoryId { get; set; }
}