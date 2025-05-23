﻿using TaskManager.Application.Commands;
using TaskManager.Application.Handlers.Commands;
using TaskManager.Application.Notifications;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.Repositories;
using MediatR;
using Moq;

namespace TaskManager.Application.Tests.Commands
{
    public class UpdateTaskCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UpdateTaskCommandHandler _handler;

        public UpdateTaskCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _mediatorMock = new Mock<IMediator>();

            _handler = new UpdateTaskCommandHandler(_userRepositoryMock.Object, _taskRepositoryMock.Object, _mediatorMock.Object);
        }
        [Fact]
        public async Task Handle_UserNotFound_Throws_NotFoundException()
        {
            //Arrange
            var command = new UpdateTaskCommand
            {
                TaskId = 1,
                Description = "Test",
                Status = TasksStatus.InProgress,
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
            var command = new UpdateTaskCommand
            {
                TaskId = 1,
                Description = "Test",
                Status = TasksStatus.InProgress,
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
            var command = new UpdateTaskCommand
            {
                TaskId = 1,
                Description = "Test",
                Status = TasksStatus.InProgress,
                UserId = 1
            };

            Tasks task = new TaskProxy(new Project(2, "test"), "Test", "", DateTime.UtcNow, TaskPriority.Low);
            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Role.User));

            _taskRepositoryMock.Setup(x => x.GetById(command.TaskId, x => x.Project)).ReturnsAsync(task);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<ForbiddenException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("You do not have permission to update this task.", exception.Message);
        }

        [Fact]
        public async Task Handle_WithValidConditions_ShouldUpdateTask()
        {
            //Arrange
            var command = new UpdateTaskCommand
            {
                TaskId = 1,
                Description = "Test",
                Status = TasksStatus.InProgress,
                UserId = 1
            };

            var user = new User(1, "test", Role.User);
            Tasks task = new TaskProxy(new ProjectProxy(user, "test"), "Test", "", DateTime.UtcNow, TaskPriority.Low);

            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Role.User));
            _taskRepositoryMock.Setup(x => x.GetById(command.TaskId, x => x.Project)).ReturnsAsync(task);

            //Act
            await _handler.Handle(command, CancellationToken.None);

            //Assert
            _taskRepositoryMock.Verify(x=>x.Update(task), Times.Once);
            _mediatorMock.Verify(x => x.Publish(It.IsAny<UpdatedTaskNotification>(), It.IsAny<CancellationToken>()), Times.Once);

        }

    }
}
