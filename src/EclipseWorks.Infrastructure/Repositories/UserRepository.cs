using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Repositories;

namespace EclipseWorks.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
