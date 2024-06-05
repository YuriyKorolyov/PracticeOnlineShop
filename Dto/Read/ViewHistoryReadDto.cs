namespace MyApp.Dto.Read
{
    /// <summary>
    /// Представляет модель данных для чтения истории просмотров.
    /// </summary>
    public class ViewHistoryReadDto
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор истории просмотров.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает дату просмотра.
        /// </summary>
        public DateTime ViewDate { get; set; }

        /// <summary>
        /// Получает или задает информацию о продукте, просмотренном пользователем.
        /// </summary>
        public ProductReadDto Product { get; set; }
    }
}
