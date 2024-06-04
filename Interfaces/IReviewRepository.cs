using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        IQueryable<Review> GetReviewsOfAProduct(int prodId);
        Task<bool> DeleteByUserId(int userId);
    }
}
