using TaskManager.Application.Handlers.Commands;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Queries;

namespace TaskManager.Application.Handlers.Queries
{
    public class GetCompletedTasksCountByUserLast30DaysHandler : IRequestHandler<GetCompletedTasksCountByUserLast30DaysQuery, GetCompletedTasksCountByUserLast30DaysQueryResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;

        public GetCompletedTasksCountByUserLast30DaysHandler(IUserRepository userRepository, ITaskRepository taskRepository)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        public async Task<GetCompletedTasksCountByUserLast30DaysQueryResponse> Handle(GetCompletedTasksCountByUserLast30DaysQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException("User not found.");

            if (user.Role != Role.Manager)
            {
                throw new ForbiddenException("User can't be permission to generate this report.");
            }

            var completedTasksPerUserDtos = await _taskRepository.GetCompletedTasksReportLast30Days();

            var average = completedTasksPerUserDtos.Count() == 0 ?
                0 :
                Math.Round(completedTasksPerUserDtos.Average(x => x.Count), 1, MidpointRounding.AwayFromZero);

            return new GetCompletedTasksCountByUserLast30DaysQueryResponse
            {
                CompletedTasksPerUser = completedTasksPerUserDtos,
                Average = average
            };
        }
    }
}
