namespace MyApp.Models
{
    /// <summary>
    /// Представляет отношение между продуктом и категорией.
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// Получает или задает идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Получает или задает связанный продукт.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Получает или задает идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Получает или задает связанную категорию.
        /// </summary>
        public Category Category { get; set; }
    }
}
