namespace MyApp.Dto.Read
{
    /// <summary>
    /// Представляет модель данных для чтения информации о категории.
    /// </summary>
    public class CategoryReadDto
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
