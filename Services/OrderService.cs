using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с заказами.
    /// </summary>
    /// <typeparam name="Order">Тип сущности заказа.</typeparam>
    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderService(IBaseRepository<Order> repository) : base(repository)
        {
        }

        /// <summary>
        /// Извлекает детали заказа по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, для которого извлекаются детали заказа.</param>
        /// <returns>Объект <see cref="IQueryable{T}"/>, представляющий коллекцию деталей заказа для указанного пользователя.</returns>
        public IQueryable<OrderDetail> GetOrderDetailsByUserId(int userId)
        {
            return GetAll()
                .Where(e => e.User.Id == userId)
                .SelectMany(c => c.OrderDetails)
                .AsNoTracking();
        }
    }
}
