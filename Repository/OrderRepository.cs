using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с заказами.
    /// </summary>
    /// <typeparam name="Order">Тип сущности заказа.</typeparam>
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OrderRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public OrderRepository(ApplicationDbContext context) : base(context) 
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
