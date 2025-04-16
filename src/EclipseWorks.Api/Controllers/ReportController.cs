﻿using EclipseWorks.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.Api.Controllers
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

    }
}
