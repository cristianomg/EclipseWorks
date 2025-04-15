﻿using EclipseWorks.Application.Commands;
using EclipseWorks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _repository;
        private readonly IMediator _mediator;

        public ProjectController(IProjectRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }
        /// <summary>
        /// Returns all projects belonging to the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllByUserId([FromHeader] int userId)
        {
            var projects = await _repository.Find(x=>x.UserId == userId);

            return Ok(projects);
        }
        /// <summary>
        /// Creates a new project for the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader] int userId, [FromBody] CreateProjectCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (userId == 0) return Unauthorized();

            command.UserId = userId;

            await _mediator.Send(command);

            return Created();
        }
        /// <summary>
        /// Deletes the user's project.
        /// </summary>
        /// <param name="projetId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{projetId}")]
        public async Task<IActionResult> Delete([FromRoute] int projetId, [FromHeader] int userId)
        {
            if (userId == 0) return Unauthorized();

            var command = new DeleteProjectCommand
            {
                UserId = userId,
                ProjectId = projetId
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
