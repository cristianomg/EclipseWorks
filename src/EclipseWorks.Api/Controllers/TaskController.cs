using EclipseWorks.Application.Commands;
using EclipseWorks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMediator _mediator;

        public TaskController(ITaskRepository taskRepository, IMediator mediator)
        {
            _taskRepository = taskRepository;
            _mediator = mediator;
        }
        /// <summary>
        /// Return all tasks of a project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>

        [HttpGet("{projectId}")] 
        public async Task<IActionResult> GetAllByProjectId([FromRoute] int projectId)
        {
            var tasks = await _taskRepository.Find(x=>x.ProjectId == projectId);

            return Ok(tasks);
        }
        /// <summary>
        /// Creates a new task for the project.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader] int userId, [FromBody] CreateTaskCommand command) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (userId == 0) return Unauthorized();

            command.UserId = userId;

            await _mediator.Send(command);

            return Created();
        }
        /// <summary>
        /// Deletes a task from the project.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Delete([FromRoute] int taskId, [FromHeader] int userId)
        {
            if (userId == 0) return Unauthorized();

            var command = new DeleteTaskCommand
            {
                UserId = userId,
                TaskId = taskId
            };


            await _mediator.Send(command);

            return NoContent();
        }
        /// <summary>
        /// Updates a task of the project.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("{taskId}")]
        public async Task<IActionResult> Update([FromRoute] int taskId, [FromHeader] int userId, [FromBody] UpdateTaskCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (userId == 0) return Unauthorized();

            command.TaskId = taskId;
            command.UserId = userId;

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
