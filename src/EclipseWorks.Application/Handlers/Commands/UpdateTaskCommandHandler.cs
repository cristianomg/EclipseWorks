using EclipseWorks.Application.Commands;
using EclipseWorks.Domain.Enums;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Repositories;
using MediatR;

namespace EclipseWorks.Application.Handlers.Commands
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommandHandler(IUserRepository userRepository, ITaskRepository taskRepository)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException("User not found.");
            var task = await _taskRepository.GetById(request.TaskId, x => x.Project) ?? throw new NotFoundException("Task not found.");

            if (task.Project?.UserId != user.Id)
            {
                throw new ForbiddenException("You do not have permission to delete this project.");
            }

            var newDescription = string.IsNullOrEmpty(request.Description) ? task.Description : request.Description;
            TasksStatus newStatus = request.Status.HasValue ? request.Status.Value : task.Status;


            task.Update(newDescription, newStatus, user.Name);

            await _taskRepository.Update(task);
        }
    }
}
