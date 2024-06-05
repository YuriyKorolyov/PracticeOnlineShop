using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем истории просмотров.
    /// </summary>
    public interface IViewHistoryRepository : IBaseRepository<ViewHistory>
    {
        /// <summary>
        /// Получает историю просмотров для указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Последовательность объектов истории просмотров.</returns>
        IQueryable<ViewHistory> GetByUserId(int userId);

        /// <summary>
        /// Получает количество просмотров указанного продукта.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Количество просмотров продукта.</returns>
        Task<int> GetCountViewHistoryOfAProduct(int prodId, CancellationToken cancellationToken = default);
    }
}
