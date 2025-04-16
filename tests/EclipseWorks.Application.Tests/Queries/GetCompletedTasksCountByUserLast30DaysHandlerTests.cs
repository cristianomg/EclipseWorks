using EclipseWorks.Application.Handlers.Commands;
using EclipseWorks.Application.Handlers.Queries;
using EclipseWorks.Application.Queries;
using EclipseWorks.Domain.DTOs;
using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Enums;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Repositories;
using Moq;

namespace EclipseWorks.Application.Tests.Queries
{
    public class GetCompletedTasksCountByUserLast30DaysHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly GetCompletedTasksCountByUserLast30DaysHandler _handler;
        public GetCompletedTasksCountByUserLast30DaysHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _handler = new GetCompletedTasksCountByUserLast30DaysHandler(_userRepositoryMock.Object, _taskRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponse_WhenUserIsManager()
        {
            // Arrange
            var userId = 5;

            _userRepositoryMock.Setup(repo => repo.GetById(userId))
                .ReturnsAsync(new User(5, "Manager", Role.Manager));

            var completedTasks = new List<CompletedTasksPerUserDto>
        {
            new() { UserName = "Alice", Count = 5 },
            new() { UserName = "Bob", Count = 15 }
        };

            _taskRepositoryMock.Setup(repo => repo.GetCompletedTasksReportLast30Days())
                .ReturnsAsync(completedTasks);

            var request = new GetCompletedTasksCountByUserLast30DaysQuery { UserId = userId };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.CompletedTasksPerUser.Count);
            Assert.Equal(10, result.Average);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 5;

            _userRepositoryMock.Setup(repo => repo.GetById(userId))
                .ReturnsAsync((User)null);

            var request = new GetCompletedTasksCountByUserLast30DaysQuery { UserId = userId };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, CancellationToken.None));

            Assert.Equal("User not found.", exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldThrowForbiddenException_WhenUserIsNotManager()
        {
            // Arrange
            var userId = 1;

            _userRepositoryMock.Setup(repo => repo.GetById(userId))
                .ReturnsAsync(new User(1, "User", Role.User));

            var request = new GetCompletedTasksCountByUserLast30DaysQuery { UserId = userId };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ForbiddenException>(() =>
                _handler.Handle(request, CancellationToken.None));
            
            Assert.Equal("User can't be permission to generate this report.", exception.Message);
        }
    }
}
