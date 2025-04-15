using EclipseWorks.Application.Commands;
using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Enums;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Repositories;
using MediatR;

namespace EclipseWorks.Application.Handlers.Commands
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectCommandHandler(IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }
        public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException("User not found.");
            var project = await _projectRepository.GetById(request.ProjectId, x=>x.Tasks) ?? throw new NotFoundException("Task not found.");


            if (project.UserId != user.Id)
            {
                throw new ForbiddenException("You do not have permission to delete this project.");
            }
            
            if (HasIncompleteTasks(project.Tasks))
            {
                throw new PendingTaskException(
                    $"The project cannot be deleted because it has {CountIncompleteTasks(project.Tasks)} incomplete task(s). " +
                    $"Please complete or delete all pending tasks before proceeding."
                );
            }

            await _projectRepository.Remove(project);
        }
        private bool HasIncompleteTasks(IEnumerable<Tasks> tasks)
        {
            return tasks.Any(x => x.Status != TasksStatus.Completed);
        }
        private int CountIncompleteTasks(IEnumerable<Tasks> tasks)
        {
            return tasks.Count(x => x.Status != TasksStatus.Completed);
        }
    }
}
