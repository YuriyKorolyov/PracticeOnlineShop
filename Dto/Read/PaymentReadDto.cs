using MyApp.Models;

namespace MyApp.Dto.Read
{
    /// <summary>
    /// Представляет модель данных для чтения информации о платеже.
    /// </summary>
    public class PaymentReadDto
    {
        /// <summary>
        /// Получает или задает идентификатор платежа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает сумму платежа.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Получает или задает дату платежа.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Получает или задает статус платежа.
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// Получает или задает информацию о промокоде, связанном с платежом.
        /// </summary>
        public PromoCodeReadDto PromoCode { get; set; }

        /// <summary>
        /// Получает или задает идентификатор заказа, связанного с платежом.
        /// </summary>
        public int OrderId { get; set; }
    }
}
