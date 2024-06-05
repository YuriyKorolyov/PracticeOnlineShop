namespace MyApp.Dto.Update
{
    /// <summary>
    /// Представляет модель данных для обновления отзыва.
    /// </summary>
    public class ReviewUpdateDto
    {
        /// <summary>
        /// Получает или задает идентификатор отзыва.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает текст отзыва.
        /// </summary>
        public string ReviewText { get; set; }

        /// <summary>
        /// Получает или задает рейтинг отзыва.
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
