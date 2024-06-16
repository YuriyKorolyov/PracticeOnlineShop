using MyApp.Models;

namespace MyApp.Dto.ExportToExcel
{
    public class ProductCategoryExcelDto
    {
        /// <summary>
        /// Получает или задает идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор категории.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
