namespace Game.Application.Features.Games.Queries.GetCurrentQuestion;

public readonly record struct GetCurrentQuestionQueryResponse(string QuestionText, IEnumerable<AnswerDto> Answers);