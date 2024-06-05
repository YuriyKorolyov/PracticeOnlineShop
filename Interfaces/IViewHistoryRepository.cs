using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IViewHistoryRepository : IBaseRepository<ViewHistory>
    {
        IQueryable<ViewHistory> GetByUserId(int userId);
        Task<int> GetCountViewHistoryOfAProduct(int prodId, CancellationToken cancellationToken = default);
    }
}
