namespace MyApp.Dto.Create
{
    /// <summary>
    /// Представляет модель данных для создания нового платежа.
    /// </summary>
    public class PaymentCreateDto
    {
        /// <summary>
        /// Получает или задает название промокода.
        /// </summary>
        public string PromoName { get; set; }

        /// <summary>
        /// Получает или задает идентификатор заказа, к которому относится платеж.
        /// </summary>
        public int OrderId { get; set; }
    }
}
