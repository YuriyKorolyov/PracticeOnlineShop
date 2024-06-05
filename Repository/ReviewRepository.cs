using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с отзывами.
    /// </summary>
    /// <typeparam name="Review">Тип сущности отзыва.</typeparam>
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ReviewRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Удаляет отзывы пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns><see langword="true"/>, если удаление успешно; в противном случае — <see langword="false"/>.</returns>
        public async Task<bool> DeleteByUserId(int userId, CancellationToken cancellationToken = default)
        {
            var reviews = await GetAll().Where(c => c.User.Id == userId).ToListAsync();
            _context.Reviews.RemoveRange(reviews);

            return await SaveAsync(cancellationToken);
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
