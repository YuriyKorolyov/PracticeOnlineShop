namespace MyApp.Dto.Update
{
    /// <summary>
    /// Представляет модель данных для обновления корзины.
    /// </summary>
    public class CartUpdateDto
    {
        /// <summary>
        /// Получает или задает количество товаров в корзине.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Получает или задает идентификатор продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }
    }
}
