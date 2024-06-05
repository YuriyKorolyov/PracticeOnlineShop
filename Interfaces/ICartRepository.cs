using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        IQueryable<Cart> GetByUserId(int userId);
        Task<bool> DeleteByUserId(int userId, CancellationToken cancellationToken = default);
    }
}
