using MyApp.Models;

namespace MyApp.Dto.Update
{
    /// <summary>
    /// Представляет модель данных для обновления платежа.
    /// </summary>
    public class PaymentUpdateDto
    {
        /// <summary>
        /// Получает или задает идентификатор платежа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает статус платежа.
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// Получает или задает название промокода.
        /// </summary>
        public string PromoName { get; set; }

        /// <summary>
        /// Получает или задает идентификатор заказа.
        /// </summary>
        public int OrderId { get; set; }
    }
}
