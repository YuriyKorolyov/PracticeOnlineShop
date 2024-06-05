using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с историей просмотров.
    /// </summary>
    /// <typeparam name="ViewHistory">Тип сущности истории просмотров.</typeparam>
    public class ViewHistoryRepository : BaseRepository<ViewHistory>, IViewHistoryRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ViewHistoryRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public ViewHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получает количество записей истории просмотров для указанного продукта.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Количество записей истории просмотров для указанного продукта.</returns>
        public async Task<int> GetCountViewHistoryOfAProduct(int prodId, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .Where(vh => vh.Product.Id == prodId)
                .CountAsync(cancellationToken);
        }

        /// <summary>
        /// Получает историю просмотров для указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>История просмотров для указанного пользователя.</returns>
        public IQueryable<ViewHistory> GetByUserId(int userId)
        {
            return GetAll()
                .Where(vh => vh.User.Id == userId)
                .Include(vh => vh.Product)
                .AsQueryable();
        }
    }
}
