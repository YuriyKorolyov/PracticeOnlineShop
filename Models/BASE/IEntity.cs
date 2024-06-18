namespace MyApp.Models.BASE
{
    /// <summary>
    /// Представляет базовую сущность с идентификатором.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Получает или задает идентификатор сущности.
        /// </summary>
        int Id { get; set; }
    }
}
