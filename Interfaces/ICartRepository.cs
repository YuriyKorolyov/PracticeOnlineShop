using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetCartsByUserIdAsync(int userId);
        Task AddToCartAsync(Cart cart);
        Task RemoveFromCartAsync(int cartId);
        Task ClearCartAsync(int userId);
        Task UpdateCartAsync(Cart cart);
    }
}
