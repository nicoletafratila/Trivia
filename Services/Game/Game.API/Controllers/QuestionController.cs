using Common.ViewModels;
using Common.ViewModels.Pagination;
using Game.Application.Features.Questions.Commands.AddQuestion;
using Game.Application.Features.Questions.Commands.DeleteQuestion;
using Game.Application.Features.Questions.Queries.GetQuestions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TriviaCsv.Services.Game.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly ISender _mediator;

        public QuestionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PagedList<QuestionModel>> GetAllAsync([FromQuery] QueryParameters queryStringParameters)
        {
            GetQuestionsQuery query = new()
            {
                QueryParameters = queryStringParameters
            };
            return await _mediator.Send(query);
        }

        [HttpPost("")]
        public async Task<AddQuestionCommandResponse> PostAsync(AddQuestionCommand addQuestionCommand)
        {
            return await _mediator.Send(addQuestionCommand);
        }

        [HttpDelete("{id}")]
        public async Task<DeleteQuestionCommandResponse> DeleteAsync(long id)
        {
            var response = new DeleteQuestionCommandResponse();
            var command = new DeleteQuestionCommand();
            command.QuestionId = id;
            try
            {
                await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}

