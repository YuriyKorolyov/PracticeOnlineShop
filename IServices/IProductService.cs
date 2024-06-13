using MyApp.IServices.BASE;
using MyApp.Models;

namespace MyApp.IServices
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем продуктов.
    /// </summary>
    public interface IProductService : IBaseService<Product>
    {
        /// <summary>
        /// Получает продукт по его имени.
        /// </summary>
        /// <param name="productName">Имя продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Продукт.</returns>
        Task<Product> GetByNameAsync(string productName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает рейтинг продукта по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Рейтинг продукта.</returns>
        Task<decimal> GetProductRatingAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает информацию о продукте из DTO, переводя название в верхний регистр.
        /// </summary>
        /// <param name="productName">Название продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит значение true, если продукт существует, иначе false.</returns>
        Task<bool> ExistsByNameAsync(string productName, CancellationToken cancellationToken = default);
    }
}
