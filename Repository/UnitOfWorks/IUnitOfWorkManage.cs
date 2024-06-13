using MyApp.Data;
using System.Reflection;

namespace MyApp.Repository.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationDbContext GetContext(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
