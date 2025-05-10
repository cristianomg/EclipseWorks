using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Commands;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Workers
{
    public class CheckTaskDueDateWorker : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer? _timer = null;
        private readonly ILogger<CheckTaskDueDateWorker> _logger; 
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public CheckTaskDueDateWorker(ILogger<CheckTaskDueDateWorker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            using (var scope = _serviceScopeFactory.CreateAsyncScope())
            {
                var _taskRepository = scope.ServiceProvider.GetRequiredService<ITaskRepository>();
                var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var now = DateTime.UtcNow;
                var tasksToMarkDelayed = await _taskRepository.Find(x => 
                    x.Status != TasksStatus.Completed &&
                    x.Delayed == false && 
                    x.DueDate <= now, 
                    x=>x.Project!
                );

                foreach (var task in tasksToMarkDelayed)
                {
                    task.MarkAsDelayed();

                    await _mediator.Send(new AddNotificationCommand
                    {
                        Message = $"Tarefa '{task.Title}' no projeto '{task.Project!.Name}' está atrasada.",
                        Users = new int[] { task.Project!.UserId },
                        RedirectUrl = $"/project/{task.Project.Id}?taskId={task.Id}"
                    });

                    await _taskRepository.Update(task);
                }
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckTaskDueDateWorker finish.");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
