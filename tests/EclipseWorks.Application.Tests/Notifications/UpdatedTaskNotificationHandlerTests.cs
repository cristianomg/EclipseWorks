using EclipseWorks.Application.Handlers.Notifications;
using EclipseWorks.Application.Notifications;
using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseWorks.Application.Tests.Notifications
{
    public class UpdatedTaskNotificationHandlerTests
    {
        private readonly Mock<ITaskHistoryRepository> _taskHistoryRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<UpdatedTaskNotificationHandler>> _loggerMock;
        private readonly UpdatedTaskNotificationHandler _handler;

        public UpdatedTaskNotificationHandlerTests()
        {
             _taskHistoryRepositoryMock = new Mock<ITaskHistoryRepository>();
            var _serviceProviderMock = new Mock<IServiceProvider>();    
            var _scopeMock = new Mock<IServiceScope>(); 
            var _serviceScopeFactory = new Mock<IServiceScopeFactory>();
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<UpdatedTaskNotificationHandler>>();  
            

            _serviceProviderMock.Setup(s => s.GetService(typeof(ITaskHistoryRepository))).Returns(_taskHistoryRepositoryMock.Object);
            _scopeMock.Setup(s=>s.ServiceProvider).Returns(_serviceProviderMock.Object);
            _serviceScopeFactory.Setup(s=>s.CreateScope()).Returns(_scopeMock.Object);

            _handler = new UpdatedTaskNotificationHandler(_mediatorMock.Object, _loggerMock.Object, _serviceScopeFactory.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturn_WhenRetryLimitIsExceeded()
        {
            //Arrange
            var notification = new UpdatedTaskNotification
            {
                TaskId = 1,
                UpdatedByUser = "john.doe",
                RetryCount = 6,
                OldData = new Dictionary<string, string?>
                {
                    { "title", "Old Title" }
                },
                NewData = new Dictionary<string, string?>
                {
                    { "title", "New Title" }
                }
            };

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _taskHistoryRepositoryMock.Verify(r => r.Add(It.IsAny<TaskHistory>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturn_WhenNoChangesApplied()
        {
            //Arrange
            var notification = new UpdatedTaskNotification
            {
                TaskId = 1,
                UpdatedByUser = "john.doe",
                RetryCount = 6,
                OldData = new Dictionary<string, string?>
                {
                    { "title", "Old Title" }
                },
                NewData = new Dictionary<string, string?>
                {
                    { "title", "Old Title" }
                }
            };

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _taskHistoryRepositoryMock.Verify(r => r.Add(It.IsAny<TaskHistory>()), Times.Never);
        }


        [Fact]
        public async Task Handle_ShouldAddHistory_WhenThereAreChanges()
        {
            //Arrange
            var notification = new UpdatedTaskNotification
            {
                TaskId = 1,
                UpdatedByUser = "john.doe",
                RetryCount = 0,
                OldData = new Dictionary<string, string?>
                {
                    { "title", "Old Title" }
                },
                NewData = new Dictionary<string, string?>
                {
                    { "title", "New Title" }
                }
            };

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _taskHistoryRepositoryMock.Verify(r => r.Add(It.IsAny<TaskHistory>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldRetry_WhenErrorIsThrown()
        {
            //Arrange
            var notification = new UpdatedTaskNotification
            {
                TaskId = 1,
                UpdatedByUser = "john.doe",
                RetryCount = 0,
                OldData = new Dictionary<string, string?>
                {
                    { "title", "Old Title" }
                },
                NewData = new Dictionary<string, string?>
                {
                    { "title", "New Title" }
                }
            };

            _taskHistoryRepositoryMock.Setup(r => r.Add(It.IsAny<TaskHistory>())).ThrowsAsync(new Exception("Teste"));

            // Act
            await _handler.Handle(notification, CancellationToken.None);

            // Assert
            _mediatorMock.Verify(r => r.Publish(notification, It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
