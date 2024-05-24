using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ReviewRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateReviewAsync(Review review)
        {
            _context.Add(review);
            return await Save();
        }

        public async Task<bool> DeleteReviewAsync(Review review)
        {
            _context.Remove(review);
            return await Save();
        }

        public async Task<bool> DeleteReviewsAsync(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return await Save();
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Review>> GetReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsOfAProductAsync(int prodId)
        {
            return await _context.Reviews.Where(r => r.Product.Id == prodId).ToListAsync();
        }

        public async Task<bool> ReviewExistsAsync(int reviewId)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == reviewId);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateReviewAsync(Review review)
        {
            _context.Update(review);
            return await Save();
        }

        public async Task<Review> GetReviewsTrimToUpperAsync(ReviewDto reviewCreate)
        {
            var reviews = await GetReviewsAsync();
            return reviews.Where(c => c.ReviewText.Trim().ToUpper() == reviewCreate.ReviewText.TrimEnd().ToUpper()).FirstOrDefault();
        }
    }
}
