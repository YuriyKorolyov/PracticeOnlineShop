using MyApp.Models;

namespace MyApp.Dto.Read
{
    /// <summary>
    /// Представляет модель данных для чтения информации о заказе.
    /// </summary>
    public class OrderReadDto
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
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Получает или задает информацию о платеже, связанном с заказом.
        /// </summary>
        public PaymentReadDto Payment { get; set; }

        /// <summary>
        /// Получает или задает коллекцию деталей заказа, связанных с данным заказом.
        /// </summary>
        public ICollection<OrderDetailReadDto> OrderDetails { get; set; }
    }
}
