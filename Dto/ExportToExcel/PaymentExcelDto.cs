namespace MyApp.Dto.ExportToExcel
{
    /// <summary>
    /// DTO для экспорта данных платежа в Excel.
    /// </summary>
    public class PaymentExcelDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает сумму оплаты.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Получает или задает дату оплаты.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Получает или задает статус оплаты.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Получает или задает идентификатор заказа, к которому относится данная оплата.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор промокода, связанного с данной оплатой.
        /// </summary>
        public int? PromoId { get; set; }
    }
}
