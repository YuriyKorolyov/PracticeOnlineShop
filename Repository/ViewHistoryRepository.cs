using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class ViewHistoryRepository : IViewHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ViewHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateViewHistoryAsync(ViewHistory viewHistory)
        {
            _context.Add(viewHistory);
            return await SaveAsync();
        }

        public async Task<bool> DeleteViewHistoryAsync(List<ViewHistory> viewHistories)
        {
            _context.RemoveRange(viewHistories);
            return await SaveAsync();
        }

        public async Task<int> GetCountViewHistoryOfAProductAsync(int prodId)
        {
            return await _context.ViewHistories.Where(vh => vh.Product.Id == prodId).CountAsync();
        }

        public async Task<ViewHistory> GetViewHistoryByIdAsync(int viewId)
        {
            return await _context.ViewHistories.Where(vh => vh.Id == viewId).Include(vh => vh.Product).FirstOrDefaultAsync();
        }

        public IQueryable<ViewHistory> GetViewHistoryByUserId(int userId)
        {
            return _context.ViewHistories
                .Where(vh => vh.User.Id == userId)
                .Include(vh => vh.Product)
                .AsQueryable();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateViewHistoryAsync(ViewHistory viewHistory)
        {
            _context.Update(viewHistory);
            return await SaveAsync();
        }

        public async Task<bool> ViewHistoryExistsAsync(int vhId)
        {
            return await _context.ViewHistories.AnyAsync(vh => vh.Id == vhId);
        }
    }
}
