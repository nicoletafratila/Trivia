using Game.Application.Interfaces.Persistence;
using Game.Domain.Entities;
using MediatR;

namespace Game.Application.Features.Questions.Commands.AddQuestion;

public class AddQuestionCommandHandler : IRequestHandler<AddQuestionCommand, AddQuestionCommandResponse>
{
    private readonly IQuestionsRepository _questionsRepository;

    public AddQuestionCommandHandler(IQuestionsRepository questionsRepository)
    {
        _questionsRepository = questionsRepository;
    }

    public async Task<AddQuestionCommandResponse> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new Question()
            .WithText(request.Text)
            .WithCategory(request.CategoryId)
            .WithIncorrectAnswers(request.IncorrectAnswers)
            .WithCorrectAnswer(request.CorrectAnswer);
        question = await _questionsRepository.AddAsync(question);
        return new AddQuestionCommandResponse { QuestionId = question.Id };
    }
}