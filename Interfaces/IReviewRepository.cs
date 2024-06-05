using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем отзывов.
    /// </summary>
    public interface IReviewRepository : IBaseRepository<Review>
    {
        /// <summary>
        /// Получает отзывы о продукте по его идентификатору.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <returns>Отзывы о продукте.</returns>
        IQueryable<Review> GetReviewsOfAProduct(int prodId);

        /// <summary>
        /// Удаляет отзывы пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><c>true</c>, если отзывы пользователя удалены успешно, иначе <c>false</c>.</returns>
        Task<bool> DeleteByUserId(int userId, CancellationToken cancellationToken = default);
    }
}
