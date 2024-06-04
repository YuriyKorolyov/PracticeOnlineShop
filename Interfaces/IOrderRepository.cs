using MyApp.Dto;
using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>  
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByUserId(int userId);
    }
}
