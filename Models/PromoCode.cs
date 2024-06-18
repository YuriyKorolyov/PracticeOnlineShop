using MyApp.Models.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет промо-код.
    /// </summary>
    public class PromoCode : IEntity
    {
        /// <summary>
        /// Получает или задает идентификатор промо-кода.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название промо-кода.
        /// </summary>
        public string PromoName { get; set; }

        /// <summary>
        /// Получает или задает скидку, предоставляемую промо-кодом.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Получает или задает дату начала действия промо-кода.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Получает или задает дату окончания действия промо-кода.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Получает или задает коллекцию платежей, связанных с данным промо-кодом.
        /// </summary>
        public ICollection<Payment> Payments { get; set; }
    }
}
