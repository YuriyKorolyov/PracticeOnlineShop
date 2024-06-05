using MyApp.Dto;
using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>  
    {
        IQueryable<OrderDetail> GetOrderDetailsByUserId(int userId);
    }
}
