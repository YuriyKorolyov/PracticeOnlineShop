using Microsoft.EntityFrameworkCore.Storage;
using MyApp.Data;

namespace MyApp.Repository.UnitOfWorks
{
    /// <summary>
    /// Реализация интерфейса Unit of Work для управления транзакциями и контекстом базы данных.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed = false;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UnitOfWork"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если контекст базы данных не предоставлен.</exception>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Получает экземпляр контекста базы данных приложения.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Экземпляр контекста базы данных.</returns>
        public ApplicationDbContext GetContext(CancellationToken cancellationToken = default)
        {
            return _context;
        }

        /// <summary>
        /// Начинает новую транзакцию с использованием контекста базы данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Асинхронную задачу для выполнения.</returns>
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// Фиксирует (коммитит) текущую транзакцию в базе данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Асинхронную задачу для выполнения.</returns>
        /// <exception cref="InvalidOperationException">Вызывается, если транзакция не была начата.</exception>
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            try
            {
                await SaveAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                }
            }
        }

        /// <summary>
        /// Откатывает текущую транзакцию в базе данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Асинхронную задачу для выполнения.</returns>
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync(cancellationToken);
                    await _transaction.DisposeAsync();
                }
            }
            finally
            {
                _transaction = null;
            }
        }

        /// <summary>
        /// Сохраняет все изменения, сделанные в контексте базы данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Флаг успешности операции сохранения.</returns>
        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// <summary>
        /// Освобождает ресурсы, используемые этим классом.
        /// </summary>
        /// <param name="disposing">True, если вызван метод Dispose явно; в противном случае False.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.DisposeAsync().AsTask().GetAwaiter().GetResult(); // Ensure transaction is disposed synchronously
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Освобождает все ресурсы, используемые этим экземпляром класса.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
