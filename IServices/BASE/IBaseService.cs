using MyApp.Models.BASE;
using System.Linq.Expressions;

namespace MyApp.IServices.BASE
{
    /// Интерфейс базового сервиса для работы с сущностями.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности.</typeparam>
    public interface IBaseService<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Проверяет существование сущности по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>True, если сущность с указанным идентификатором существует; в противном случае - false.</returns>
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает все сущности.
        /// </summary>
        /// <returns>Запрос IQueryable, представляющий все сущности типа TEntity.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Получает сущность по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Задача, возвращающая сущность типа TEntity.</returns>
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает сущности по условию.
        /// </summary>
        /// <param name="condition">Условие выборки сущности.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <param name="includeProperties">Связанные сущности для включения.</param>
        /// <returns>Задача, возвращающая сущность типа TEntity.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Получает сущность по ее идентификатору с возможностью дополнительного включения связанных сущностей с использованием функции Include.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="include">Функция для включения связанных сущностей.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Задача, возвращающая сущность типа TEntity.</returns>
        Task<TEntity> GetByIdAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает список сущностей по их идентификаторам.
        /// </summary>
        /// <param name="ids">Идентификаторы сущностей.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Задача, возвращающая список сущностей типа TEntity.</returns>
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавляет новую сущность.
        /// </summary>
        /// <param name="entity">Добавляемая сущность типа TEntity.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Задача.</returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        /// <param name="entity">Обновляемая сущность типа TEntity.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Задача.</returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        /// <param name="entity">Удаляемая сущность типа TEntity.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Задача.</returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет сущность по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Задача.</returns>
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет список сущностей по их идентификаторам.
        /// </summary>
        /// <param name="ids">Идентификаторы удаляемых сущностей.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Задача.</returns>
        Task DeleteByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    }
}
