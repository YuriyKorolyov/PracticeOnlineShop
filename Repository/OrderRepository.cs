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
        public IQueryable<OrderDetail> GetOrderDetailsByUserId(int userId)
        {
            return GetAll()
                .Where(e => e.User.Id == userId)
                .SelectMany(c => c.OrderDetails)
                .AsNoTracking();
        }
    }
}
