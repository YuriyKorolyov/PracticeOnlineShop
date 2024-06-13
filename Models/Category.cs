using MyApp.Repository.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет сущность категории.
    /// </summary>
    public class Category : IEntity
    {
        /// <summary>
        /// Получает или задает идентификатор категории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название категории.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Получает или задает коллекцию связей между категорией и продуктами.
        /// </summary>
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
