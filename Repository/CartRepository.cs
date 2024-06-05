using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с корзиной.
    /// </summary>
    /// <typeparam name="Cart">Тип сущности корзины.</typeparam>
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CartRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Удаляет все элементы корзины, связанные с указанным идентификатором пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, элементы корзины которого следует удалить.</param>
        /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит логическое значение, указывающее на успешность операции.</returns>
        public async Task<bool> DeleteByUserId(int userId, CancellationToken cancellationToken = default)
        {
            var cartItems = await GetAll().Where(c => c.User.Id == userId).ToListAsync(cancellationToken);
            _context.Carts.RemoveRange(cartItems);

            return await SaveAsync();
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
