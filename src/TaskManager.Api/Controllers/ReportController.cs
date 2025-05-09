using TaskManager.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("CompletedTasksCountByUserLast30Days")]
        public async Task<IActionResult> GetCompletedTasksCountByUserLast30Days([FromHeader] int userId)
        {
            var query = new GetCompletedTasksCountByUserLast30DaysQuery
            {
                UserId = userId
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("DelayedTasksByUsers")]
        public async Task<IActionResult> GetDelayerdTasksByUsers([FromHeader] int userId)
        {
            var query = new GetDelayerdTasksByUsersQuery
            {
                UserId = userId
            };

            return Ok(await _mediator.Send(query));
        }
    }
}
