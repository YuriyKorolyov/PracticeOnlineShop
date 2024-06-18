namespace MyApp.Dto.ExportToExcel
{
    /// <summary>
    /// DTO для экспорта данных продукта-категории в Excel.
    /// </summary>
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
