using MyApp.IServices.BASE;
using MyApp.Models;

namespace MyApp.IServices
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем заказов.
    /// </summary>
    public interface IOrderService : IBaseService<Order>
    {
        /// <summary>
        /// Получает детали заказов по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Последовательность деталей заказов пользователя.</returns>
        IQueryable<OrderDetail> GetOrderDetailsByUserId(int userId);
    }
}
