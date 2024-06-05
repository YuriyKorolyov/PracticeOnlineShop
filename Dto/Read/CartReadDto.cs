namespace MyApp.Dto.Read
{
    /// <summary>
    /// Представляет модель данных для чтения информации о корзине.
    /// </summary>
    public class CartReadDto
    {
        /// <summary>
        /// Получает или задает идентификатор корзины.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает количество продуктов в корзине.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Получает или задает информацию о продукте, находящемся в корзине.
        /// </summary>
        public ProductReadDto Product { get; set; }
    }
}
