using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с отзывами.
    /// </summary>
    /// <typeparam name="Review">Тип сущности отзыва.</typeparam>
    public class ReviewService : BaseService<Review>, IReviewService
    {
        public ReviewService(IBaseRepository<Review> repository) : base(repository)
        {
        }

        /// <summary>
        /// Удаляет отзывы пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns><see langword="true"/>, если удаление успешно; в противном случае — <see langword="false"/>.</returns>
        public async Task DeleteByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            var reviews = await GetAll()
                .Where(r => r.User.Id == userId)
                .Select(r => r.Id)
                .ToListAsync(cancellationToken);

            await DeleteByIdsAsync(reviews);
        }

        /// <summary>
        /// Получает отзывы о продукте.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <returns>Отзывы о продукте.</returns>
        public IQueryable<Review> GetReviewsOfAProduct(int prodId)
        {
            return GetAll()
                .Where(r => r.Product.Id == prodId)
                .AsQueryable();
        }
    }
}
