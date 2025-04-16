using EclipseWorks.Application.Commands;
using EclipseWorks.Application.Handlers.Commands;
using EclipseWorks.Application.Notifications;
using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Enums;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Repositories;
using MediatR;
using Moq;
using System.Xml.Linq;

namespace EclipseWorks.Application.Tests.Commands
{
    public class AddCommentCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AddCommentCommandHandler _handler;

        public AddCommentCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new AddCommentCommandHandler(_userRepositoryMock.Object, _taskRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_Throws_NotFoundException()
        {
            //Arrange
            var command = new AddCommentCommand
            {
                Comment = "Test",
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
            var command = new AddCommentCommand
            {
                Comment = "Test",
                TaskId = 1,
                UserId = 1
            };

            _userRepositoryMock.Setup(r => r.GetById(command.UserId)).ReturnsAsync(new User(1, "Teste", Role.User));

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Task not found.", exception.Message);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("       ")]
        public async Task Handle_CommentRequired_Throws_CommentRequiredException(string? comment)
        {
            //Arrange
            var command = new AddCommentCommand
            {
                Comment = comment,
                TaskId = 1,
                UserId = 1
            };

            _userRepositoryMock.Setup(r => r.GetById(command.UserId)).ReturnsAsync(new User(1, "Teste", Role.User));
            _taskRepositoryMock.Setup(r => r.GetById(command.TaskId, x => x.Comments)).ReturnsAsync(new Tasks(1, "Test", "Test", DateTime.UtcNow, TaskPriority.Low));

            //Act & Assert
            var exception = await Assert.ThrowsAsync<CommentRequiredException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("The comment is required.", exception.Message);
        }
        [Fact]
        public async Task Handle_ShouldBeCreateComment_When_CommentIsValid()
        {
            //Arrange
            var command = new AddCommentCommand
            {
                Comment = "Test",
                TaskId = 1,
                UserId = 1
            };
            var task = new Tasks(1, "Test", "Test", DateTime.UtcNow, TaskPriority.Low);
            _userRepositoryMock.Setup(r => r.GetById(command.UserId)).ReturnsAsync(new User(1, "Teste", Role.User));
            _taskRepositoryMock.Setup(r => r.GetById(command.TaskId, x => x.Comments)).ReturnsAsync(task);

            //Act
           await _handler.Handle(command, CancellationToken.None);

            //Assert
            _taskRepositoryMock.Verify(x => x.Update(task), Times.Once);
            _mediatorMock.Verify(x => x.Publish(It.IsAny<UpdatedTaskNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
