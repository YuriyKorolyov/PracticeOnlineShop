using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces.BASE;
using System.Linq.Expressions;

namespace MyApp.Repository.BASE
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        //public IQueryable<TEntity> GetAll()
        //{
        //    return _dbSet.AsQueryable();
        //}
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
        public async Task<TEntity> GetById(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<TEntity> GetById(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _dbSet.Where(e => e.Id == id);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = _dbSet.Where(e => e.Id == id);

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<bool> Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            return await SaveAsync(cancellationToken);
        }

        public async Task<bool> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return await SaveAsync(cancellationToken);
        }

        public async Task<bool> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            return await SaveAsync(cancellationToken);
        }

        public async Task<bool> DeleteById(int id, CancellationToken cancellationToken = default)
        {
            //var entity = _dbSet.Attach((TEntity)Activator.CreateInstance(typeof(TEntity), id)).Entity;
            TEntity entity = new TEntity { Id = id };
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            
            return await SaveAsync(cancellationToken);
        }

        public async Task<bool> DeleteByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {           
            //var entities = ids.Select(id => (TEntity)Activator.CreateInstance(typeof(TEntity), id)).ToList();
            var entities = ids.Select(id => new TEntity { Id = id }).ToList();
            _dbSet.AttachRange(entities);
            _dbSet.RemoveRange(entities);

            return await SaveAsync(cancellationToken);
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(c => ids.Contains(c.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
