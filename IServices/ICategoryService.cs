using MyApp.IServices.BASE;
using MyApp.Models;

namespace MyApp.IServices
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем категорий.
    /// </summary>
    public interface ICategoryService : IBaseService<Category>
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
        /// <param name="categoryName">Название категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит значение true, если категория существует, иначе false.</returns>
        Task<bool> ExistsByNameAsync(string categoryName, CancellationToken cancellationToken = default);
    }
}
