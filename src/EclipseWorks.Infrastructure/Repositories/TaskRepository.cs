using EclipseWorks.Domain.DTOs;
using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Enums;
using EclipseWorks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EclipseWorks.Infrastructure.Repositories
{
    public class TaskRepository : Repository<Tasks>, ITaskRepository
    {
        public TaskRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<CompletedTasksPerUserDto>> GetCompletedTasksReportLast30Days()
        {
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

            var tasks = await _dbSet
                .Where(t => t.Status == TasksStatus.Completed)
                .Where(t => t.Histories
                    .OrderByDescending(h => h.CreatedAt)
                    .Any(h => h.Changes.Any(c =>
                        c.Field == nameof(Tasks.Status) &&
                        c.NewValue == TasksStatus.Completed.ToString() &&
                        h.CreatedAt >= thirtyDaysAgo)))
                .Include(t => t.Histories)
                    .ThenInclude(h => h.Changes)
                .Include(t => t.Project)
                    .ThenInclude(p => p.User)
                 .GroupBy(x=>x.Project.User)
                 .Select(x=> new CompletedTasksPerUserDto { UserName = x.Key.Name, Count = x.Count() })
                 .ToListAsync();

            return tasks;
        }
    }
}
