using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetCartsByUserId(int userId);
        Task AddToCart(Cart cart);
        Task RemoveFromCart(int cartId);
        Task ClearCart(int userId);
        Task UpdateCart(Cart cart);
    }
}
