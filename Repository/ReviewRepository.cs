using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateReviewAsync(Review review)
        {
            _context.Add(review);
            return await SaveAsync();
        }

        public async Task<bool> DeleteReviewAsync(Review review)
        {
            _context.Remove(review);
            return await SaveAsync();
        }

        public async Task<bool> DeleteReviewsAsync(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return await SaveAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _context.Reviews.Where(r => r.Id == reviewId).Include(r => r.User).FirstOrDefaultAsync();
        }

        public IQueryable<Review> GetReviews()
        {
            return _context.Reviews
                .Include(r => r.User)
                .AsQueryable();
        }

        public IQueryable<Review> GetReviewsOfAProduct(int prodId)
        {
            return _context.Reviews
                .Where(r => r.Product.Id == prodId)
                .AsQueryable();
        }

        public async Task<bool> ReviewExistsAsync(int reviewId)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == reviewId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateReviewAsync(Review review)
        {
            _context.Update(review);
            return await SaveAsync();
        }
    }
}
