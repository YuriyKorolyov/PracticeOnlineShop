using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByIdAsync(int cartId);
        IQueryable<Cart> GetCartsByUserId(int userId);
        Task AddToCartAsync(Cart cart);
        Task RemoveFromCartAsync(int cartId);
        Task ClearCartAsync(int userId);
        Task UpdateCartAsync(Cart cart);
    }
}
