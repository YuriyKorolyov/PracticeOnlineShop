using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с пользователями.
    /// </summary>
    /// <typeparam name="User">Тип сущности пользователя.</typeparam>
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
