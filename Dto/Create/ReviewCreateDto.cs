namespace MyApp.Dto.Create
{
    /// <summary>
    /// Представляет модель данных для создания нового отзыва.
    /// </summary>
    public class ReviewCreateDto
    {
        /// <summary>
        /// Получает или задает текст отзыва.
        /// </summary>
        public string ReviewText { get; set; }

        /// <summary>
        /// Получает или задает рейтинг, присвоенный продукту в отзыве.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Получает или задает идентификатор пользователя, оставившего отзыв.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор продукта, к которому относится отзыв.
        /// </summary>
        public int ProductId { get; set; }
    }
}
