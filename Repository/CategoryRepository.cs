using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с категориями продуктов.
    /// </summary>
    /// <typeparam name="Category">Тип сущности категории.</typeparam>
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Извлекает продукты, связанные с указанной категорией.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории, для которой извлекаются продукты.</param>
        /// <returns>Объект <see cref="IQueryable{T}"/>, представляющий коллекцию продуктов для указанной категории.</returns>
        public IQueryable<Product> GetProductsByCategory(int categoryId)
        {
            return _context.ProductCategories
                .Where(e => e.CategoryId == categoryId)
                .Select(c => c.Product)
                .AsNoTracking();
        }

        /// <summary>
        /// Извлекает категорию по имени с удалением пробелов в начале и конце строки и преобразованием к верхнему регистру.
        /// </summary>
        /// <param name="categoryCreate">Данные для создания категории.</param>
        /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит объект категории.</returns>
        public async Task<Category> GetCategoryTrimToUpperAsync(CategoryCreateDto categoryCreate, CancellationToken cancellationToken = default)
        {
            return await GetAll().Where(c => c.CategoryName.Trim().ToUpper() == categoryCreate.CategoryName.TrimEnd().ToUpper()).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
