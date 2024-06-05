namespace MyApp.Dto.Update
{
    /// <summary>
    /// Представляет модель данных для обновления категории.
    /// </summary>
    public class CategoryUpdateDto
    {
        /// <summary>
        /// Получает или задает идентификатор категории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название категории.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
