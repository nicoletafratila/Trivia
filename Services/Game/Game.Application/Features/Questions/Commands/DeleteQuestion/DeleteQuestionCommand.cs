using MediatR;

namespace Game.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommand : IRequest
    {
        public long QuestionId { get; set; }
    }
}
