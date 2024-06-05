using MyApp.Dto.Create;
using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем продуктов.
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product>
    {
        /// <summary>
        /// Получает продукт по его имени.
        /// </summary>
        /// <param name="productName">Имя продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Продукт.</returns>
        Task<Product> GetByName(string productName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает рейтинг продукта по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Рейтинг продукта.</returns>
        Task<decimal> GetProductRating(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает информацию о продукте из DTO, переводя название в верхний регистр.
        /// </summary>
        /// <param name="productCreate">DTO для создания продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Продукт.</returns>
        Task<Product> GetProductTrimToUpperAsync(ProductCreateDto productCreate, CancellationToken cancellationToken = default);
    }
}
