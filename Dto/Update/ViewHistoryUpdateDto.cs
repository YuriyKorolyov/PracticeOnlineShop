namespace MyApp.Dto.Update
{
    /// <summary>
    /// Представляет модель данных для обновления истории просмотров.
    /// </summary>
    public class ViewHistoryUpdateDto
    {
        /// <summary>
        /// Получает или задает идентификатор истории просмотров.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает дату просмотра.
        /// </summary>
        public DateTime ViewDate { get; set; }

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
