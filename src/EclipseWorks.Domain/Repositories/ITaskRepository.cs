using EclipseWorks.Domain.DTOs;
using EclipseWorks.Domain.Entities;

namespace EclipseWorks.Domain.Repositories
{
    public interface  ITaskRepository : IRepository<Tasks>
    {
        public Task<List<CompletedTasksPerUserDto>> GetCompletedTasksReportLast30Days();
    }
}
