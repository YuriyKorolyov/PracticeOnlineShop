namespace MyApp.Dto.ExportToExcel
{
    /// <summary>
    /// DTO для экспорта данных деталей заказа в Excel.
    /// </summary>
    public class OrderDetailExcelDto
    {
        /// <summary>
        /// Получает или задает идентификатор детали заказа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает количество деталей заказа.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Получает или задает цену за единицу товара.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Получает или задает заказ, к которому относится данная деталь заказа.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Получает или задает товар, связанный с данной деталью заказа.
        /// </summary>
        public int ProductId { get; set; }
    }
}
