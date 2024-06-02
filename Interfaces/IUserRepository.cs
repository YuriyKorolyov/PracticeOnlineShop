using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<bool> UserExistsAsync(int userId);
        Task<bool> SaveAsync();
    }
}
