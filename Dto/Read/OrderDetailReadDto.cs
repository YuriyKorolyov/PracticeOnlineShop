namespace MyApp.Dto.Read
{
    /// <summary>
    /// Представляет модель данных для чтения информации о деталях заказа.
    /// </summary>
    public class OrderDetailReadDto
    {
        /// <summary>
        /// Получает или задает идентификатор детали заказа.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает количество продукта в детали заказа.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Получает или задает цену за единицу продукта в детали заказа.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Получает или задает информацию о продукте, связанном с деталью заказа.
        /// </summary>
        public ProductReadDto Product { get; set; }
    }
}
