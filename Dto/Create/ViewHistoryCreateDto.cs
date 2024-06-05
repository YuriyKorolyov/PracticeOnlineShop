namespace MyApp.Dto.Create
{
    /// <summary>
    /// Представляет модель данных для создания новой истории просмотров.
    /// </summary>
    public class ViewHistoryCreateDto
    {
        /// <summary>
        /// Получает или задает дату просмотра.
        /// </summary>
        public DateTime ViewDate { get; set; }

        /// <summary>
        /// Получает или задает идентификатор продукта, который был просмотрен.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор пользователя, который просмотрел продукт.
        /// </summary>
        public int UserId { get; set; }
    }
}
