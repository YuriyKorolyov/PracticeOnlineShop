namespace MyApp.Dto.ExportToExcel
{
    /// <summary>
    /// DTO для экспорта данных категории в Excel.
    /// </summary>
    public class CategoryExcelDto
    {
        /// <summary>
        /// Получает или задает идентификатор категории.
        /// </summary>
        /// <value>Идентификатор категории.</value>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название категории.
        /// </summary>
        /// <value>Название категории.</value>
        public string CategoryName { get; set; }
    }
}
