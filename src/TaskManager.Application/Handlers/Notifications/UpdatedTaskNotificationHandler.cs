using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Notifications;

namespace TaskManager.Application.Handlers.Notifications
{
    public class UpdatedTaskNotificationHandler : INotificationHandler<UpdatedTaskNotification>
    {
        private const int MAX_RETRY_LIMIT = 5;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _scopeFactory;


        public UpdatedTaskNotificationHandler(IMediator mediator, ILogger<UpdatedTaskNotificationHandler> logger, IServiceScopeFactory scopeFactory)
        {
            _mediator = mediator;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task Handle(UpdatedTaskNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                if (notification.RetryCount > MAX_RETRY_LIMIT)
                {
                    return;
                }

                using var scope = _scopeFactory.CreateScope();
                var taskHistoryRepository = scope.ServiceProvider.GetRequiredService<ITaskHistoryRepository>();


                var history = new TaskHistory(notification.TaskId, notification.UpdatedByUser);

                foreach (var (key, newValue) in notification.NewData)
                {
                    notification.OldData.TryGetValue(key, out var oldValue);

                    if (oldValue != newValue)
                    {
                        history.Changes.Add(new TaskHistoryChange(key, oldValue, newValue));
                    }
                }

                if (history.Changes.Count > 0)
                {
                    await taskHistoryRepository.Add(history);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing handler {HandlerName}. Error: {ErrorMessage}", nameof(UpdatedTaskNotification), ex.Message);

                notification.RetryCount += 1;

                _ = _mediator.Publish(notification, cancellationToken);

            }

        }
    }
}
