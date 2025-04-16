using EclipseWorks.Application.Commands;
using EclipseWorks.Application.Notifications;
using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Repositories;
using MediatR;

namespace EclipseWorks.Application.Handlers.Commands
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMediator _mediator;

        public AddCommentCommandHandler(IUserRepository userRepository, ITaskRepository taskRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _mediator = mediator;
        }

        public async Task Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException("User not found.");
            var task = await _taskRepository.GetById(request.TaskId, x => x.Comments) ?? throw new NotFoundException("Task not found.");

            if (string.IsNullOrEmpty(request.Comment) || string.IsNullOrWhiteSpace(request.Comment))
            {
                throw new CommentRequiredException("The comment is required.");
            }

            task.Comments.Add(new TaskComment(request.Comment));

            await _taskRepository.Update(task);

            _ = _mediator.Publish(MapToUpdatedTaskNotification(request, user.Name), cancellationToken);
        }

        private UpdatedTaskNotification MapToUpdatedTaskNotification(AddCommentCommand request, string updatedByUser)
        {
            const string COMMENT_FIELD_NAME = "Comment";
            return new UpdatedTaskNotification
            {
                TaskId = request.TaskId,
                OldData = new Dictionary<string, string?>
            {
                { COMMENT_FIELD_NAME, null },
            },
                NewData = new Dictionary<string, string?>
            {
                { COMMENT_FIELD_NAME, request.Comment },
            },
                UpdatedByUser = updatedByUser,
                RetryCount = 0
            };
        }
    }
}
