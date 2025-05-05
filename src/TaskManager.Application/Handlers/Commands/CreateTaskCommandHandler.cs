using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;
using MediatR;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Commands
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private const int MAX_TASKS_PER_PROJECT = 20;
        public CreateTaskCommandHandler(IUserRepository userRepository, IProjectRepository projectRepository, ITaskRepository taskRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }
        public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException("User not found.");
            var project = await _projectRepository.GetById(request.ProjectId, x => x.Tasks) ?? throw new NotFoundException("Project not found.");

            if (user.Id != project.UserId)
            {
                throw new ForbiddenException("You do not have permission to create task in this project.");
            }

            var hasReachedTaskLimit = project.Tasks.Count() >= MAX_TASKS_PER_PROJECT;

            if (hasReachedTaskLimit)
            {
                throw new TaskLimitExceededException(MAX_TASKS_PER_PROJECT);
            }

            var task = new Tasks(project.Id, request.Title, request.Description, request.DueDate, request.Priority);

            await _taskRepository.Add(task);
        }
    }
}
