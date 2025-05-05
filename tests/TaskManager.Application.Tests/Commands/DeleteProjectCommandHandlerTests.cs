using TaskManager.Application.Commands;
using TaskManager.Application.Handlers.Commands;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;
using Moq;

namespace TaskManager.Application.Tests.Commands
{
    public class DeleteProjectCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly DeleteProjectCommandHandler _handler;

        public DeleteProjectCommandHandlerTests()
        {
            _userRepositoryMock= new Mock<IUserRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _handler = new DeleteProjectCommandHandler(_userRepositoryMock.Object, _projectRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_Throws_NotFoundException()
        {
            //Arrange
            var command = new DeleteProjectCommand
            {
                ProjectId = 1,
                UserId = 1
            };

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("User not found.", exception.Message);
        }
        [Fact]
        public async Task Handle_ProjectNotFound_Throws_NotFoundException()
        {
            //Arrange
            var command = new DeleteProjectCommand
            {
                ProjectId = 1,
                UserId = 1
            };

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Domain.Enums.Role.User));

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Project not found.", exception.Message);
        }
        [Fact]
        public async Task Handle_UserDontHavePermission_Throws_ForbiddenException()
        {
            //Arrange
            var command = new DeleteProjectCommand
            {
                ProjectId = 1,
                UserId = 1
            };

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Domain.Enums.Role.User));
            _projectRepositoryMock.Setup(x => x.GetById(command.ProjectId, x=>x.Tasks)).ReturnsAsync(new Project(2, "Test"));

            //Act & Assert
            var exception = await Assert.ThrowsAsync<ForbiddenException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("You do not have permission to delete this project.", exception.Message);
        }
        [Fact]
        public async Task Handle_WithIncompleteTask_Throws_PendingTaskException()
        {
            //Arrange
            var command = new DeleteProjectCommand
            {
                ProjectId = 1,
                UserId = 1
            };

            var project = new Project(1, "Test");
            project.Tasks.Add(new Tasks(project.Id, "Test", "Test", DateTime.UtcNow, Domain.Enums.TaskPriority.Low));

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Domain.Enums.Role.User));
            _projectRepositoryMock.Setup(x => x.GetById(command.ProjectId, x => x.Tasks)).ReturnsAsync(project);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<PendingTaskException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"The project cannot be deleted because it has 1 incomplete task(s). " +
                         $"Please complete or delete all pending tasks before proceeding.", exception.Message);
        }

        [Fact]
        public async Task Handle_WithValidConditions_ShouldDeleteProject()
        {
            //Arrange
            var command = new DeleteProjectCommand
            {
                ProjectId = 1,
                UserId = 1
            };

            var project = new Project(1, "Test");

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Domain.Enums.Role.User));
            _projectRepositoryMock.Setup(x => x.GetById(command.ProjectId, x => x.Tasks)).ReturnsAsync(project);

            //Act & Assert
            await _handler.Handle(command, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.Remove(project), Times.Once);
        }
    }
}
