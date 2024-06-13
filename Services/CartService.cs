using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с корзиной.
    /// </summary>
    /// <typeparam name="Cart">Тип сущности корзины.</typeparam>
    public class CartService : BaseService<Cart>, ICartService
    {
        public CartService(IBaseRepository<Cart> repository) : base(repository)
        {
        }

        /// <summary>
        /// Удаляет все элементы корзины, связанные с указанным идентификатором пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, элементы корзины которого следует удалить.</param>
        /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит логическое значение, указывающее на успешность операции.</returns>
        public async Task DeleteByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            var cartItems = await GetAll().Where(c => c.User.Id == userId)
                .Select(c => c.Id)
                .ToListAsync(cancellationToken);

            await DeleteByIdsAsync(cartItems, cancellationToken);
        }

        /// <summary>
        /// Извлекает все элементы корзины для указанного идентификатора пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, для которого следует извлечь элементы корзины.</param>
        /// <returns>Объект <see cref="IQueryable{T}"/>, представляющий коллекцию элементов корзины для указанного пользователя.</returns>
        public IQueryable<Cart> GetByUserId(int userId)
        {
            return GetAll()
                .Where(cart => cart.User.Id == userId)
                .Include(cart => cart.Product)
                .AsQueryable();
        }
    }
}
