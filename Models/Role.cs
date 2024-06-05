using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет роль пользователя.
    /// </summary>
    public class Role : IEntity
    {
        /// <summary>
        /// Получает или задает идентификатор роли.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название роли.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Получает или задает список пользователей, имеющих данную роль.
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}
