namespace MyApp.Dto.ExportToExcel
{
    /// <summary>
    /// DTO для экспорта данных заказа в Excel.
    /// </summary>
    public class OrderExcelDto
    {
        /// <summary>
        /// Получает или задает идентификатор заказа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает дату заказа.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Получает или задает общую сумму заказа.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Получает или задает статус заказа.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Получает или задает пользователя, сделавшего заказ.
        /// </summary>
        public int UserId { get; set; }
    }
}
