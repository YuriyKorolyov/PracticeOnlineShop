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
        Task<bool> Exists(int id);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> GetById(int id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null);
        Task<IEnumerable<TEntity>> GetByIds(IEnumerable<int> ids);
        Task<bool> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> DeleteById(int id);
        Task<bool> DeleteByIds(IEnumerable<int> ids);
        Task<bool> SaveAsync();
    }
}
