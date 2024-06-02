using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users
                .Include(u => u.Role)
                .AsQueryable();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.Where(o => o.Id == userId).Include(u => u.Role).FirstOrDefaultAsync();
        }

        public async Task<bool> AddUserAsync(User user)
        {
            await _context.AddAsync(user);
            return await SaveAsync();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return await SaveAsync();
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Remove(user);
            return await SaveAsync();
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
