using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models.BASE;
using MyApp.Repository.UnitOfWorks;
using System.Linq.Expressions;

namespace MyApp.Repository.BASE
{
    /// <summary>
    /// Репозиторий для базовых операций с сущностями.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        /// <inheritdoc/>
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = _unitOfWork.GetContext();
            _dbSet = _context.Set<TEntity>();
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken cancellationToken = default, 
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _dbSet.Where(condition);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(
            int id,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet.Where(e => e.Id == id);

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        /// <inheritdoc/>
        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await _dbSet.Where(e=>e.Id == id).ExecuteDeleteAsync(cancellationToken);            
        }

        /// <inheritdoc/>
        public async Task DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {           
            await _dbSet.Where(e => ids.Contains(e.Id)).ExecuteDeleteAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(c => ids.Contains(c.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
