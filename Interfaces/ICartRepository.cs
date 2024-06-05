using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем корзин.
    /// </summary>
    public interface ICartRepository : IBaseRepository<Cart>
    {
        /// <summary>
        /// Получает корзины по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Последовательность корзин пользователя.</returns>
        IQueryable<Cart> GetByUserId(int userId);

        /// <summary>
        /// Удаляет корзины по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>True, если корзины были успешно удалены; в противном случае — false.</returns>
        Task<bool> DeleteByUserId(int userId, CancellationToken cancellationToken = default);
    }
}
