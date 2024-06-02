using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IReviewRepository
    {
        IQueryable<Review> GetReviews();
        Task<Review> GetReviewByIdAsync(int reviewId);
        IQueryable<Review> GetReviewsOfAProduct(int prodId);
        Task<bool> ReviewExistsAsync(int reviewId);
        Task<bool> CreateReviewAsync(Review review);
        Task<bool> UpdateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(Review review);
        Task<bool> DeleteReviewsAsync(List<Review> reviews);
        Task<bool> SaveAsync();
    }
}
