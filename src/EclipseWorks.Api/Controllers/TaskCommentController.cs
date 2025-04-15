using EclipseWorks.Application.Commands;
using EclipseWorks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCommentController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMediator _mediator;

        public TaskCommentController(ITaskRepository taskRepository, IMediator mediator)
        {
            _taskRepository = taskRepository;
            _mediator = mediator;
        }
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetCommentsByTaskId([FromRoute] int taskId)
        {
            var task = await _taskRepository.GetById(taskId, x => x.Comments);

            if (task == null) return NotFound();

            return Ok(task.Comments);
        }

        [HttpPost("{taskId}")]
        public async Task<IActionResult> AddComment([FromRoute] int taskId, [FromHeader] int userId, [FromBody] AddCommentCommand command)
        {
            command.UserId = userId;
            command.TaskId = taskId;

            await _mediator.Send(command);

            return Created();
        }
    }
}
