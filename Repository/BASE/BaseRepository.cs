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

        public async Task<bool> Exists(int id)
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
        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<TEntity> GetById(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _dbSet.Where(e => e.Id == id);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync();
        }
        public async Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            IQueryable<TEntity> query = _dbSet.Where(e => EF.Property<int>(e, "Id") == id);

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            return await SaveAsync();
        }

        public Task<bool> Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return SaveAsync();
        }

        public async Task<bool> Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteById(int id)
        {
            //var entity = _dbSet.Attach((TEntity)Activator.CreateInstance(typeof(TEntity), id)).Entity;
            TEntity entity = new TEntity { Id = id };
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            
            return await SaveAsync();
        }

        public async Task<bool> DeleteByIds(IEnumerable<int> ids)
        {           
            //var entities = ids.Select(id => (TEntity)Activator.CreateInstance(typeof(TEntity), id)).ToList();
            var entities = ids.Select(id => new TEntity { Id = id }).ToList();
            _dbSet.AttachRange(entities);
            _dbSet.RemoveRange(entities);

            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<int> ids)
        {
            return await _dbSet
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }
    }
}
