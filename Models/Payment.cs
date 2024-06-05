using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет сущность оплаты.
    /// </summary>
    public class Payment : IEntity
    {
        /// <summary>
        /// Получает или задает идентификатор оплаты.
        /// </summary>
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
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// Получает или задает идентификатор заказа, к которому относится данная оплата.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Получает или задает заказ, связанный с данной оплатой.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Получает или задает идентификатор промокода, связанного с данной оплатой.
        /// </summary>
        public int? PromoId { get; set; }

        /// <summary>
        /// Получает или задает промокод, связанный с данной оплатой.
        /// </summary>
        public PromoCode PromoCode { get; set; }
    }

    /// <summary>
    /// Перечисление статусов оплаты.
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Ожидание оплаты.
        /// </summary>
        Pending,

        /// <summary>
        /// Успешная оплата.
        /// </summary>
        Success,

        /// <summary>
        /// Оплата не удалась.
        /// </summary>
        Failed
    }
}
