using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;
using MediatR;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        public CreateProjectCommandHandler(IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }
        public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new NotFoundException("User not found.");

            var nameAlreadyInUse = await _projectRepository.Any(x => x.Name == request.Name && x.UserId == request.UserId);

            if (nameAlreadyInUse)
            {
                throw new ProjectNameInUseException(request.Name);
            }

            var project = new Project(request.UserId, request.Name);

            await _projectRepository.Add(project);
        }
    }
}
