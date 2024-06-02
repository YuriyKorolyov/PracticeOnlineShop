using MyApp.Dto;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrders();
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByUserAsync(int userId);
        Task<bool> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Order order);
        Task<bool> SaveAsync();
        Task<bool> OrderExistsAsync(int id);
    }
}
