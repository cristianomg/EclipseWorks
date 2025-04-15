using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Repositories;

namespace EclipseWorks.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DataContext context) : base(context)
        {
        }
    }
}
