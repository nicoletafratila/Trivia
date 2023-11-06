using Common.ViewModels;
using Game.Application.Features.Games.Queries.GetGameCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TriviaCsv.Services.Game.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _mediator;

        public CategoryController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return await _mediator.Send(new GetGameCategoriesQuery());
        }
    }
}
