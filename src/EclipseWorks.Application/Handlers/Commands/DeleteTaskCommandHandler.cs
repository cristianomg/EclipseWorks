using EclipseWorks.Application.Commands;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Repositories;
using MediatR;

namespace EclipseWorks.Application.Handlers.Commands
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(IUserRepository userRepository, ITaskRepository taskRepository)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }


        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException("User not found.");
            var task = await _taskRepository.GetById(request.TaskId, x => x.Project) ?? throw new NotFoundException("Task not found.");

            if (task.Project?.UserId != user.Id)
            {
                throw new ForbiddenException("You do not have permission to delete this project.");
            }

            await _taskRepository.Remove(task);
        }
    }
}
