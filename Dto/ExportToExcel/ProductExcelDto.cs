using MyApp.Models;

namespace MyApp.Dto.ExportToExcel
{
    public class ProductExcelDto
    {
        /// <summary>
        /// Получает или задает идентификатор продукта.
        /// </summary>
        public int Id { get; set; }

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
        /// Получает или задает количество продукта в наличии.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Получает или задает URL изображения продукта.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
