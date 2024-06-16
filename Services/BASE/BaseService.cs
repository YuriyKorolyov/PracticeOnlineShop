using MyApp.Data;
using MyApp.IServices.BASE;
using MyApp.Repository.BASE;
using System.Linq.Expressions;

namespace MyApp.Services.BASE
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IEntity
    {
        public IBaseRepository<TEntity> Repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Repository.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Repository.DeleteAsync(entity, cancellationToken);
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await Repository.DeleteByIdAsync(id, cancellationToken);
        }

        public async Task DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            await Repository.DeleteByIdsAsync(ids, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Repository.ExistsAsync(id, cancellationToken);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Repository.GetAsync(condition, cancellationToken, includeProperties);
        }

        public async Task<TEntity> GetByIdAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include, CancellationToken cancellationToken = default)
        {
            return await Repository.GetByIdAsync(id, include, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            return await Repository.GetByIdsAsync(ids, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
