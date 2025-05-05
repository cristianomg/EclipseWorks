using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories;
public interface ITaskRepository : IRepository<Tasks>
{
    public Task<List<CompletedTasksPerUserDto>> GetCompletedTasksReportLast30Days();
}
