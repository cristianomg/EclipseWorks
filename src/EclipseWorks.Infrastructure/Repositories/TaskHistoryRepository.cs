using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Repositories;

namespace EclipseWorks.Infrastructure.Repositories
{
    public class TaskHistoryRepository : Repository<TaskHistory>, ITaskHistoryRepository
    {
        public TaskHistoryRepository(DataContext context) : base(context)
        {
        }
    }
}
