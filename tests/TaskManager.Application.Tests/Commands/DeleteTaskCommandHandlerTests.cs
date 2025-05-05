using TaskManager.Application.Commands;
using TaskManager.Application.Handlers.Commands;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;
using Moq;

namespace TaskManager.Application.Tests.Commands
{
    public class DeleteTaskCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly DeleteTaskCommandHandler _handler;
        public DeleteTaskCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _handler = new DeleteTaskCommandHandler(_userRepositoryMock.Object, _taskRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_Throws_NotFoundException()
        {
            //Arrange
            var command = new DeleteTaskCommand
            {
                TaskId = 1,
                UserId = 1
            };

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("User not found.", exception.Message);
        }
        [Fact]
        public async Task Handle_TaskNotFound_Throws_NotFoundException()
        {
            //Arrange
            var command = new DeleteTaskCommand
            {
                TaskId = 1,
                UserId = 1
            };

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Role.User));

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Task not found.", exception.Message);
        }
        [Fact]
        public async Task Handle_UserDontHavePermission_Throws_ForbiddenException()
        {
            //Arrange
            var command = new DeleteTaskCommand
            {
                TaskId = 1,
                UserId = 1
            };

            Tasks task = new TaskProxy(new Project(2, "test"), "Test", "", DateTime.UtcNow, TaskPriority.Low);
            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Role.User));

            _taskRepositoryMock.Setup(x => x.GetById(command.TaskId, x => x.Project)).ReturnsAsync(task);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<ForbiddenException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("You do not have permission to delete this task.", exception.Message);
        }

        [Fact]
        public async Task Handle_WithValidConditions_ShouldDeleteTask()
        {
            //Arrange
            var command = new DeleteTaskCommand
            {
                TaskId = 1,
                UserId = 0
            };

            var user = new User(1, "test", Role.User);
            Tasks task = new TaskProxy(new ProjectProxy(user, "test"), "Test", "", DateTime.UtcNow, TaskPriority.Low);

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Role.User));
            _taskRepositoryMock.Setup(x => x.GetById(command.TaskId, x => x.Project)).ReturnsAsync(task);

            //Act & Assert
            await _handler.Handle(command, CancellationToken.None);
            _taskRepositoryMock.Verify(x => x.Remove(task), Times.Once);

        }
    }

    public class ProjectProxy: Project
    {
        private readonly User _internalUser;

        public ProjectProxy(User user, string name) : base(user.Id, name)
        {
            _internalUser = user;
        }
        public override User? User => _internalUser;
    }
    public class TaskProxy : Tasks
    {
        private readonly Project _internalProject;
        public TaskProxy(Project project, string title, string? description, DateTime dueDate, TaskPriority priority) : base(project.Id, title, description, dueDate, priority)
        {
            _internalProject = project;
        }

        public override Project? Project => _internalProject;
    }
}
