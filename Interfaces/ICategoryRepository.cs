using MyApp.Dto.Create;
using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем категорий.
    /// </summary>
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        /// <summary>
        /// Получает продукты по идентификатору категории.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <returns>Последовательность продуктов, относящихся к указанной категории.</returns>
        IQueryable<Product> GetProductsByCategory(int categoryId);

        /// <summary>
        /// Получает категорию по данным из объекта создания категории.
        /// </summary>
        /// <param name="categoryCreate">Данные для создания категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Объект категории, полученный из данных создания категории.</returns>
        Task<Category> GetCategoryTrimToUpperAsync(CategoryCreateDto categoryCreate, CancellationToken cancellationToken = default);
    }
}
