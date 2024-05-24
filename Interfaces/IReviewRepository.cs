using MyApp.Dto;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IReviewRepository
    {
        Task<ICollection<Review>> GetReviewsAsync();
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<ICollection<Review>> GetReviewsOfAProductAsync(int prodId);
        Task<bool> ReviewExistsAsync(int reviewId);
        Task<bool> CreateReviewAsync(Review review);
        Task<bool> UpdateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(Review review);
        Task<bool> DeleteReviewsAsync(List<Review> reviews);
        Task<bool> Save();
        Task<Review> GetReviewsTrimToUpperAsync(ReviewDto reviewCreate);
    }
}
