namespace MyApp.Dto.Read
{
    /// <summary>
    /// Представляет модель данных для чтения информации об отзыве.
    /// </summary>
    public class ReviewReadDto
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
        /// Получает или задает рейтинг, присвоенный отзыву.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Получает или задает информацию о пользователе, написавшем отзыв.
        /// </summary>
        public UserReadDto User { get; set; }
    }
}
