using MyApp.Models.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет сущность детали заказа.
    /// </summary>
    public class OrderDetail : IEntity
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
        public Order Order { get; set; }

        /// <summary>
        /// Получает или задает товар, связанный с данной деталью заказа.
        /// </summary>
        public Product Product { get; set; }

        public OrderDetail() { }

        public OrderDetail(int quantity, decimal unitPrice, Product product)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            Product = product;
        }
    }
}
