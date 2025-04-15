using EclipseWorks.Application.Commands;
using EclipseWorks.Application.Notifications;
using EclipseWorks.Domain.Entities;
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
        private readonly IMediator _mediator;

        public UpdateTaskCommandHandler(IUserRepository userRepository, ITaskRepository taskRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _mediator = mediator;
        }

        public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException("User not found.");
            var task = await _taskRepository.GetById(request.TaskId, x => x.Project) ?? throw new NotFoundException("Task not found.");

            if (task.Project?.UserId != user.Id)
            {
                throw new ForbiddenException("You do not have permission to update this task.");
            }

            var historyNotification = MapToUpdatedTaskNotification(task, request, user.Name);

            var newDescription = string.IsNullOrEmpty(request.Description) ? task.Description : request.Description;
            TasksStatus newStatus = request.Status.HasValue ? request.Status.Value : task.Status;

            task.Update(newDescription, newStatus, user.Name);

            await _taskRepository.Update(task);

            _ = _mediator.Publish(historyNotification, cancellationToken);
        }
        private UpdatedTaskNotification MapToUpdatedTaskNotification(Tasks task, UpdateTaskCommand request, string updatedByUser)
        {
            return new UpdatedTaskNotification
            {
                TaskId = request.TaskId,
                OldData = new Dictionary<string, string?>
            {
                { nameof(task.Description), task.Description },
                { nameof(task.Status), task.Status.ToString() }
            },
                NewData = new Dictionary<string, string?>
            {
                { nameof(task.Description), request.Description },
                { nameof(task.Status), request.Status.ToString() }
            },
                UpdatedByUser = updatedByUser,
                RetryCount = 0
            };
        }
    }

}
