using MyApp.Models.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет сущность заказа.
    /// </summary>
    public class Order : IEntity
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
        /// Получает или задает пользователя, сделавшего заказ.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Получает или задает информацию о платеже для этого заказа.
        /// </summary>
        public Payment Payment { get; set; }

        /// <summary>
        /// Получает или задает коллекцию деталей заказа.
        /// </summary>
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Order() { }

        public Order(decimal totalAmount, OrderStatus status, User user, ICollection<OrderDetail> orderDetails)
        {
            OrderDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
            Status = status;
            User = user;
            OrderDetails = orderDetails;
        }
    }

    /// <summary>
    /// Перечисление статусов заказа.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Обработка.
        /// </summary>
        Processing,
        /// <summary>
        /// Доставка.
        /// </summary>
        Shipped,
        /// <summary>
        /// Завершен.
        /// </summary>
        Completed
    }
}
