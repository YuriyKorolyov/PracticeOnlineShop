using Microsoft.EntityFrameworkCore;
using MyApp.Dto;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<bool> UserExistsAsync(int userId);
        Task<IEnumerable<Review>> GetReviewsByUserAsync(int reviewerId);
        Task<bool> Save();
    }
}
