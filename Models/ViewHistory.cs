using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет историю просмотров.
    /// </summary>
    public class ViewHistory : IEntity
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
        /// Получает или задает пользователя, который просмотрел товар.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Получает или задает товар, который был просмотрен пользователем.
        /// </summary>
        public Product Product { get; set; }
    }

}
