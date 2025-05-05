using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TaskManager.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public Repository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public Task Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return _context.SaveChangesAsync();
        }
        public Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }
        public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query.Where(predicate).ToListAsync();
        }
        public Task<List<TEntity>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

        public Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return query.ToListAsync();
        }
        public ValueTask<TEntity?> GetById(int id)
        {
            return _dbSet.FindAsync(id);
        }

        public Task<TEntity?> GetById(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
