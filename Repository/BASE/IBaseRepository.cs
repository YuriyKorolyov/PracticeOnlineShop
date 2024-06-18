using System.Linq.Expressions;
using MyApp.Models.BASE;

namespace MyApp.Repository.BASE
{
    /// <summary>
    /// Интерфейс для базового репозитория сущностей.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Проверяет существование сущности по ее идентификатору.
        /// </summary>
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает все сущности.
        /// </summary>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Получает сущность по ее идентификатору.
        /// </summary>
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает сущность по ее идентификатору с возможностью включения связанных сущностей.
        /// </summary>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Получает сущность по ее идентификатору с возможностью дополнительного включения связанных сущностей с использованием функции Include.
        /// </summary>
        Task<TEntity> GetByIdAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает список сущностей по их идентификаторам.
        /// </summary>
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавляет новую сущность.
        /// </summary>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет сущность по ее идентификатору.
        /// </summary>
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет список сущностей по их идентификаторам.
        /// </summary>
        Task DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    }
}
