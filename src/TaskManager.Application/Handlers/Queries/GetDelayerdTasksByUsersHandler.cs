using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Queries;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Handlers.Queries
{
    public class GetDelayerdTasksByUsersHandler : IRequestHandler<GetDelayerdTasksByUsersQuery, GetDelayerdTasksByUsersQueryResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;

        public GetDelayerdTasksByUsersHandler(IUserRepository userRepository, ITaskRepository taskRepository)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        public async Task<GetDelayerdTasksByUsersQueryResponse> Handle(GetDelayerdTasksByUsersQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException("User not found.");

            if (user.Role != Role.Manager)
            {
                throw new ForbiddenException("User can't be permission to generate this report.");
            }
            var delayedTasksByUsers = await _taskRepository.GetDelayerdTasksByUsers();

            var average = delayedTasksByUsers.Count() == 0 ?
                0 :
                Math.Round(delayedTasksByUsers.Average(x => x.Count), 1, MidpointRounding.AwayFromZero);

            return new GetDelayerdTasksByUsersQueryResponse
            {
                DelayedTasksByUsers = delayedTasksByUsers,
                Average = average
            };
        }
    }
}
