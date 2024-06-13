using MyApp.Repository.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет отзыв о продукте.
    /// </summary>
    public class Review : IEntity
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
        /// Получает или задает пользователя, оставившего отзыв.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Получает или задает продукт, к которому относится отзыв.
        /// </summary>
        public Product Product { get; set; }
    }
}
