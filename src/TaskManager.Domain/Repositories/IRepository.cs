using System.Linq.Expressions;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        ValueTask<TEntity?> GetById(int id);
        Task<TEntity?> GetById(int id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
    }
}
