using MyApp.Models;
using MyApp.IServices.BASE;

namespace MyApp.IServices
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем истории просмотров.
    /// </summary>
    public interface IViewHistoryService : IBaseService<ViewHistory>
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
        Task<int> GetCountViewHistoryOfAProductAsync(int prodId, CancellationToken cancellationToken = default);
    }
}
