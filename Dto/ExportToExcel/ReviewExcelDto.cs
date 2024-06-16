using MyApp.Models;

namespace MyApp.Dto.ExportToExcel
{
    public class ReviewExcelDto
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
        public int UserId { get; set; }

        /// <summary>
        /// Получает или задает продукт, к которому относится отзыв.
        /// </summary>
        public int ProductId { get; set; }
    }
}
