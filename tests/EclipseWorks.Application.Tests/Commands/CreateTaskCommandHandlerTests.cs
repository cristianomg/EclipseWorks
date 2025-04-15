using EclipseWorks.Application.Commands;
using EclipseWorks.Application.Handlers.Commands;
using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Repositories;
using Moq;

namespace EclipseWorks.Application.Tests.Commands
{
    public class CreateTaskCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly CreateTaskCommandHandler _handler;
        public CreateTaskCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _handler = new CreateTaskCommandHandler(_userRepositoryMock.Object, _projectRepositoryMock.Object, _taskRepositoryMock.Object);
        }
        [Fact]
        public async Task Handle_UserNotFound_Throws_NotFoundException()
        {
            //Arrange
            var command = new CreateTaskCommand
            {
                Title = "TitleTest",
                Description = "Test",
                Priority = Domain.Enums.TaskPriority.Low,
                DuoDate = DateTime.UtcNow.AddDays(10),
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
            var command = new CreateTaskCommand
            {
                Title = "TitleTest",
                Description = "Test",
                Priority = Domain.Enums.TaskPriority.Low,
                DuoDate = DateTime.UtcNow.AddDays(10),
                ProjectId = 1,
                UserId = 1
            };


            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Domain.Enums.Role.User));
            
            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Project not found.", exception.Message);
        }

        [Fact]
        public async Task Handle_WhenUserIsNotTheCreator_Throws_ForbiddenException()
        {
            //Arrange
            var command = new CreateTaskCommand
            {
                Title = "TitleTest",
                Description = "Test",
                Priority = Domain.Enums.TaskPriority.Low,
                DuoDate = DateTime.UtcNow.AddDays(10),
                ProjectId = 1,
                UserId = 1
            };

            var project = new ProjectProxy(new User(2, "Usuario 2", Domain.Enums.Role.User), "Projeto teste");

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1,"Test User", Domain.Enums.Role.User));
            _projectRepositoryMock.Setup(x => x.GetById(command.ProjectId, x => x.Tasks)).ReturnsAsync(project);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<ForbiddenException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("You do not have permission to create task in this project.", exception.Message);
        }

        [Fact]
        public async Task Handle_ProjectWith20Tasks_Throws_TaskLimitExceededException()
        {
            //Arrange
            var command = new CreateTaskCommand
            {
                Title = "TitleTest",
                Description = "Test",
                Priority = Domain.Enums.TaskPriority.Low,
                DuoDate = DateTime.UtcNow.AddDays(10),
                ProjectId = 1,
                UserId = 1
            };

            var project = new Project(1, "Test");
            for (int i = 1; i <= 20; i++)
            {
                project.Tasks.Add(new Tasks(project.Id, $"Title-{i}", "Test", DateTime.UtcNow, Domain.Enums.TaskPriority.Low));
            }

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Domain.Enums.Role.User));

            _projectRepositoryMock.Setup(x => x.GetById(command.ProjectId, x => x.Tasks)).ReturnsAsync(project);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<TaskLimitExceededException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("The project cannot have more than 20 tasks.", exception.Message);
        }

        [Fact]
        public async Task Handle_WithValidConditions_ShouldCreateTask()
        {
            //Arrange
            var command = new CreateTaskCommand
            {
                Title = "TitleTest",
                Description = "Test",
                Priority = Domain.Enums.TaskPriority.Low,
                DuoDate = DateTime.UtcNow.AddDays(10),
                ProjectId = 1,
                UserId = 1
            };

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Domain.Enums.Role.User));
            _projectRepositoryMock.Setup(x => x.GetById(command.ProjectId, x=>x.Tasks)).ReturnsAsync(new Project(1, "Test"));

            //Act & Assert
            await _handler.Handle(command, CancellationToken.None);

            _taskRepositoryMock.Verify(x=>x.Add(It.IsAny<Tasks>()), Times.Once());
        }

    }
}
