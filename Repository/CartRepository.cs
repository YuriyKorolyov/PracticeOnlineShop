using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> DeleteByUserId(int userId, CancellationToken cancellationToken = default)
        {
            var cartItems = await GetAll().Where(c => c.User.Id == userId).ToListAsync(cancellationToken);
            _context.Carts.RemoveRange(cartItems);

            return await SaveAsync();
        }

        public IQueryable<Cart> GetByUserId(int userId)
        {
            return GetAll()
                .Where(cart => cart.User.Id == userId)
                .Include(cart => cart.Product)
                .AsQueryable();
        }
    }
}
