using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с продуктами.
    /// </summary>
    /// <typeparam name="Product">Тип сущности продукта.</typeparam>
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IBaseRepository<Review> _reviewRepository;

        public ProductService(IBaseRepository<Review> reviewRepository, IBaseRepository<Product> repository) : base(repository)
        {
            _reviewRepository = reviewRepository;
        }

        /// <summary>
        /// Получает продукт по его имени.
        /// </summary>
        /// <param name="productName">Имя продукта.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Продукт, соответствующий указанному имени.</returns>
        public async Task<Product> GetByNameAsync(string productName, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .Where(p => p.Name == productName)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Получает рейтинг продукта по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Рейтинг продукта.</returns>
        public async Task<decimal> GetProductRatingAsync(int id, CancellationToken cancellationToken = default)
        {
            var ratingData = await _reviewRepository.GetAll()
                                        .Where(r => r.Product.Id == id)
                                        .Select(r => r.Rating)
                                        .ToListAsync(cancellationToken);

            if (ratingData.Count == 0)
                return 0;

            return (decimal)Math.Round(ratingData.Average(), 2);
        }

        /// <summary>
        /// Получает продукт по его имени в верхнем регистре без учета окончания строки.
        /// </summary>
        /// <param name="productName">Сведения о создаваемом продукте.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит значение true, если продукт существует, иначе false.</returns>
        public async Task<bool> ExistsByNameAsync(string productName, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .AnyAsync(c => c.Name.Trim().ToUpper() == productName.TrimEnd().ToUpper(), cancellationToken);
        }
    }
}
