using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto;
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

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
            //return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.Where(o => o.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> AddUserAsync(User user)
        {
            await _context.AddAsync(user);
            return await Save();
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return await Save();
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Remove(user);
            return await Save();
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserAsync(int reviewerId)
        {
            return await _context.Reviews.Where(r => r.User.Id == reviewerId).ToListAsync();
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
