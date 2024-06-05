using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с продуктами.
    /// </summary>
    /// <typeparam name="Product">Тип сущности продукта.</typeparam>
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProductRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает продукт по его имени.
        /// </summary>
        /// <param name="productName">Имя продукта.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Продукт, соответствующий указанному имени.</returns>
        public async Task<Product> GetByName(string productName, CancellationToken cancellationToken = default)
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
        public async Task<decimal> GetProductRating(int id, CancellationToken cancellationToken = default)
        {
            var reviews = await _context.Reviews
                                        .Where(p => p.Product.Id == id)
                                        .ToListAsync(cancellationToken);

            if (!reviews.Any()) 
                return 0;

            return Math.Round((decimal)reviews.Sum(r => r.Rating) / reviews.Count(), 2);
        }

        /// <summary>
        /// Получает продукт по его имени в верхнем регистре без учета окончания строки.
        /// </summary>
        /// <param name="productCreate">Сведения о создаваемом продукте.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Продукт, соответствующий указанным данным о продукте.</returns>
        public async Task<Product> GetProductTrimToUpperAsync(ProductCreateDto productCreate, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
