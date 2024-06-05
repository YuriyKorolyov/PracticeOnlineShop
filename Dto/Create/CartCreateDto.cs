namespace MyApp.Dto.Create
{
    /// <summary>
    /// Представляет модель данных для создания новой записи корзины.
    /// </summary>
    public class CartCreateDto
    {
        /// <summary>
        /// Получает или задает количество продуктов в корзине.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Получает или задает идентификатор продукта, добавляемого в корзину.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор пользователя, которому принадлежит корзина.
        /// </summary>
        public int UserId { get; set; }
    }
}
