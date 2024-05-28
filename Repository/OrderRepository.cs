using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateOrderAsync(Order order)
        {
            _context.Add(order);
            return await SaveAsync();
        }

        public async Task<bool> DeleteOrderAsync(Order order)
        {
            _context.Remove(order);
            return await SaveAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Where(o => o.Id == id).Include(order => order.OrderDetails).ThenInclude(od => od.Product).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
            .Include(o => o.OrderDetails)
            .ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByUserAsync(int userId)
        {
            return (IEnumerable<OrderDetail>)await _context.Orders.Where(e => e.User.Id == userId).Select(c => c.OrderDetails).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            _context.Update(order);
            return await SaveAsync();
        }
        public async Task<bool> OrderExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }
    }
}
