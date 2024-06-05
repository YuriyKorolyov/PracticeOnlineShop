namespace MyApp.Dto.Create
{
    /// <summary>
    /// Представляет модель данных для создания нового продукта.
    /// </summary>
    public class ProductCreateDto
    {
        /// <summary>
        /// Получает или задает название продукта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает описание продукта.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Получает или задает цену продукта.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получает или задает количество товара на складе.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Получает или задает URL изображения продукта.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Получает или задает идентификаторы категорий, к которым относится продукт.
        /// </summary>
        public IEnumerable<int> CategoryIds { get; set; }
    }
}
