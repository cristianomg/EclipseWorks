using EclipseWorks.Application.Commands;
using EclipseWorks.Application.Handlers.Commands;
using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Exceptions;
using EclipseWorks.Domain.Repositories;
using Moq;

namespace EclipseWorks.Application.Tests.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly CreateProjectCommandHandler _handler;
        public CreateProjectCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _handler = new CreateProjectCommandHandler(_userRepositoryMock.Object, _projectRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_Throws_NotFoundException()
        {
            //Arrange
            var command = new CreateProjectCommand
            {
                Name = "Test",
                UserId = 1
            };

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("User not found.", exception.Message);
        }
        [Fact]
        public async Task Handle_ProjectNameInUse_Throws_ProjectNameInUseException()
        {
            //Arrange
            var command = new CreateProjectCommand
            {
                Name = "Test",
                UserId = 1
            };


            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Domain.Enums.Role.User));
            _projectRepositoryMock.Setup(x => x.Any(x => x.Name == command.Name)).ReturnsAsync(true);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<ProjectNameInUseException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"A project with the name {command.Name} already exists.", exception.Message);
        }
        [Fact]
        public async Task Handle_WithValidConditions_ShouldCreateProject()
        {
            //Arrange
            var command = new CreateProjectCommand
            {
                Name = "Test",
                UserId = 1
            };


            _userRepositoryMock.Setup(x => x.GetById(command.UserId)).ReturnsAsync(new User(1, "Test User", Domain.Enums.Role.User));
            _projectRepositoryMock.Setup(x => x.Any(x => x.Name == command.Name)).ReturnsAsync(false);

            //Act & Assert
            await _handler.Handle(command, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.Add(It.IsAny<Project>()), Times.Once());


        }
    }
}
