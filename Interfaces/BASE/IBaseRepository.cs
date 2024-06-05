using System.Linq.Expressions;

namespace MyApp.Interfaces.BASE
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public interface IBaseRepository<TEntity>
 where TEntity : class, IEntity
    {
        Task<bool> Exists(int id, CancellationToken cancellationToken = default);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id, CancellationToken cancellationToken = default);
        Task<TEntity> GetById(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default);
        Task<bool> Add(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> Update(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> Delete(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteById(int id, CancellationToken cancellationToken = default);
        Task<bool> DeleteByIds(IEnumerable<int> ids, CancellationToken cancellationToken = default);
        Task<bool> SaveAsync(CancellationToken cancellationToken = default);
    }
}
