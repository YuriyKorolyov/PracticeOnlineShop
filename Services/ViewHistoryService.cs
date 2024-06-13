using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с историей просмотров.
    /// </summary>
    /// <typeparam name="ViewHistory">Тип сущности истории просмотров.</typeparam>
    public class ViewHistoryService : BaseService<ViewHistory>, IViewHistoryService
    {
        public ViewHistoryService(IBaseRepository<ViewHistory> repository) : base(repository)
        {
        }

        /// <summary>
        /// Получает количество записей истории просмотров для указанного продукта.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Количество записей истории просмотров для указанного продукта.</returns>
        public async Task<int> GetCountViewHistoryOfAProductAsync(int prodId, CancellationToken cancellationToken = default)
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
