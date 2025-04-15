using EclipseWorks.Domain.Entities;
using System.Linq.Expressions;

namespace EclipseWorks.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        ValueTask<TEntity?> GetById(int id);
        Task<TEntity?> GetById(int id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
    }
}
