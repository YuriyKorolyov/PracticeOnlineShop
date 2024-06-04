using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<bool> DeleteByUserId(int userId)
        {
            var reviews = await GetAll().Where(c => c.User.Id == userId).ToListAsync();
            _context.Reviews.RemoveRange(reviews);

            return await SaveAsync();
        }

        public IQueryable<Review> GetReviewsOfAProduct(int prodId)
        {
            return GetAll()
                .Where(r => r.Product.Id == prodId)
                .AsQueryable();
        }
    }
}
