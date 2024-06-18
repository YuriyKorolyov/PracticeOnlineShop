using MyApp.Data;

namespace MyApp.Repository.UnitOfWorks
{
    /// <summary>
    /// Интерфейс для Unit of Work, который обеспечивает работу с контекстом базы данных и транзакциями.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Получает экземпляр контекста базы данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Экземпляр контекста базы данных.</returns>
        ApplicationDbContext GetContext(CancellationToken cancellationToken = default);

        /// <summary>
        /// Начинает новую транзакцию.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Асинхронную задачу для выполнения.</returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Фиксирует (коммитит) текущую транзакцию.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Асинхронную задачу для выполнения.</returns>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Откатывает текущую транзакцию.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Асинхронную задачу для выполнения.</returns>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Сохраняет все изменения в контексте базы данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Флаг успешности операции сохранения.</returns>
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
