using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    public class ViewHistoryRepository : BaseRepository<ViewHistory>, IViewHistoryRepository
    {
        public ViewHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<int> GetCountViewHistoryOfAProduct(int prodId)
        {
            return await GetAll()
                .Where(vh => vh.Product.Id == prodId)
                .CountAsync();
        }
        public IQueryable<ViewHistory> GetByUserId(int userId)
        {
            return GetAll()
                .Where(vh => vh.User.Id == userId)
                .Include(vh => vh.Product)
                .AsQueryable();
        }
    }
}
