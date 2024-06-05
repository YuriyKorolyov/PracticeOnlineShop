using System.Linq.Expressions;

namespace MyApp.Interfaces.BASE
{
    /// <summary>
    /// Представляет базовую сущность с идентификатором.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Получает или задает идентификатор сущности.
        /// </summary>
        int Id { get; set; }
    }

    /// <summary>
    /// Интерфейс для базового репозитория сущностей.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Проверяет существование сущности по ее идентификатору.
        /// </summary>
        Task<bool> Exists(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает все сущности.
        /// </summary>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Получает сущность по ее идентификатору.
        /// </summary>
        Task<TEntity> GetById(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает сущность по ее идентификатору с возможностью включения связанных сущностей.
        /// </summary>
        Task<TEntity> GetById(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Получает сущность по ее идентификатору с возможностью дополнительного включения связанных сущностей с использованием функции Include.
        /// </summary>
        Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает список сущностей по их идентификаторам.
        /// </summary>
        Task<IEnumerable<TEntity>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавляет новую сущность.
        /// </summary>
        Task<bool> Add(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        Task<bool> Update(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        Task<bool> Delete(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет сущность по ее идентификатору.
        /// </summary>
        Task<bool> DeleteById(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет список сущностей по их идентификаторам.
        /// </summary>
        Task<bool> DeleteByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Сохраняет изменения асинхронно.
        /// </summary>
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
