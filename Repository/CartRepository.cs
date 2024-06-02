using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int userId)
        {
            var cartsToRemove = await _context.Carts
                                          .Where(cart => cart.User.Id == userId)
                                          .ToListAsync();
            if (cartsToRemove.Any())
            {
                _context.Carts.RemoveRange(cartsToRemove);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _context.Carts.Where(cart => cart.Id == cartId).FirstOrDefaultAsync();
        }

        public async Task RemoveFromCartAsync(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }

        IQueryable<Cart> ICartRepository.GetCartsByUserId(int userId)
        {
            return _context.Carts
                .Include(cart => cart.Product)
                .Where(cart => cart.User.Id == userId)
                .AsQueryable();
        }
    }
}
