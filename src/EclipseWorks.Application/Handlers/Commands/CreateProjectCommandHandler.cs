using EclipseWorks.Application.Commands;
using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Repositories;
using MediatR;

namespace EclipseWorks.Application.Handlers.Commands
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

            var nameAlreadyInUse = await _projectRepository.Any(x => x.Name == request.Name);

            if (nameAlreadyInUse)
            {
                throw new ProjectNameInUseException(request.Name);
            }

            var project = new Project(request.UserId, request.Name);

            await _projectRepository.Add(project);
        }
    }
}
