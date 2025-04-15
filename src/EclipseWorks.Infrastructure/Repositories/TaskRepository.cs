using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Repositories;

namespace EclipseWorks.Infrastructure.Repositories
{
    public class TaskRepository : Repository<Tasks>, ITaskRepository
    {
        public TaskRepository(DataContext context) : base(context)
        {
        }
    }
}
