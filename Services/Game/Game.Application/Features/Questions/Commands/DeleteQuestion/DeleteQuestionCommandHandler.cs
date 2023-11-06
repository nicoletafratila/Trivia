using Game.Application.Interfaces.Persistence;
using MediatR;

namespace Game.Application.Features.Questions.Commands.DeleteQuestion;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IGameQuestionsRepository _gameQuestionsRepository;

    public DeleteQuestionCommandHandler(IQuestionsRepository questionsRepository, IGameQuestionsRepository gameQuestionsRepository)
    {
        _questionsRepository = questionsRepository;
        _gameQuestionsRepository = gameQuestionsRepository;
    }

    public async Task<Unit> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var gameQuestions = await _gameQuestionsRepository.ListAllAsync();
        if (gameQuestions == null)
            throw new Exception("There are no questions available.");

        var questionToDelete = await _questionsRepository.GetQuestionByIdIncludeAnswersAsync(request.QuestionId);
        if (questionToDelete == null)
            throw new Exception("Could not find the question you want to delete.");

        if (gameQuestions.Any(q => q.QuestionId == request.QuestionId))
        {
            throw new Exception($"The question {questionToDelete.Text} is already used in a game and can't be deleted.");
        }

        await _questionsRepository.DeleteAsync(questionToDelete);
        return default;
    }
}