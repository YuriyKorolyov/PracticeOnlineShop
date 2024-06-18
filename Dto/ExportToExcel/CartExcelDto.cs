namespace MyApp.Dto.ExportToExcel
{
    /// <summary>
    /// DTO для экспорта данных корзины в Excel.
    /// </summary>
    public class CartExcelDto
    {
        /// <summary>
        /// Получает или задает идентификатор корзины.
        /// </summary>
        /// <value>Идентификатор корзины.</value>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает количество товара в корзине.
        /// </summary>
        /// <value>Количество товара.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// Получает или задает идентификатор продукта.
        /// </summary>
        /// <value>Идентификатор продукта.</value>
        public int ProductId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор пользователя.
        /// </summary>
        /// <value>Идентификатор пользователя.</value>
        public int UserId { get; set; }
    }
}
