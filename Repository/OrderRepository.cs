using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) 
        {
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByUserId(int userId)
        {
            return (IEnumerable<OrderDetail>)await GetAll().Where(e => e.User.Id == userId).Select(c => c.OrderDetails).ToListAsync();
        }
    }
}
