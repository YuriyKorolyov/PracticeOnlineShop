using MyApp.IServices.BASE;
using MyApp.Models.BASE;
using MyApp.Repository.BASE;
using System.Linq.Expressions;

namespace MyApp.Services.BASE
{
    /// <summary>
    /// Базовый сервис для работы с сущностями <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности, с которой работает сервис.</typeparam>
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IEntity
    {
        public IBaseRepository<TEntity> Repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseService{TEntity}"/>.
        /// </summary>
        /// <param name="repository">Репозиторий, используемый сервисом для доступа к данным.</param>
        public BaseService(IBaseRepository<TEntity> repository)
        {
            Repository = repository;
        }

        /// <inheritdoc/>
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Repository.AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Repository.DeleteAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await Repository.DeleteByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            await Repository.DeleteByIdsAsync(ids, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Repository.ExistsAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Repository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Repository.GetAsync(condition, cancellationToken, includeProperties);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include, CancellationToken cancellationToken = default)
        {
            return await Repository.GetByIdAsync(id, include, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            return await Repository.GetByIdsAsync(ids, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
