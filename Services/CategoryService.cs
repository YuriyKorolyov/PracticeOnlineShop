using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с категориями продуктов.
    /// </summary>
    /// <typeparam name="Category">Тип сущности категории.</typeparam>
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IBaseRepository<Category> repository) : base(repository)
        {
        }

        /// <summary>
        /// Извлекает продукты, связанные с указанной категорией.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории, для которой извлекаются продукты.</param>
        /// <returns>Объект <see cref="IQueryable{T}"/>, представляющий коллекцию продуктов для указанной категории.</returns>
        public IQueryable<Product> GetProductsByCategory(int categoryId)
        {
            return GetAll()
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.ProductCategories)
                .Select(pc => pc.Product)
                .AsNoTracking();
        }

        /// <summary>
        /// Извлекает категорию по имени с удалением пробелов в начале и конце строки и преобразованием к верхнему регистру.
        /// </summary>
        /// <param name="categoryName">Название категории.</param>
        /// <param name="cancellationToken">Токен для отслеживания запросов на отмену.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит значение true, если категория существует, иначе false.</returns>
        public async Task<bool> ExistsByNameAsync(string categoryName, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .AnyAsync(c => c.CategoryName.Trim().ToUpper() == categoryName.TrimEnd().ToUpper(), cancellationToken);
        }
    }
}
