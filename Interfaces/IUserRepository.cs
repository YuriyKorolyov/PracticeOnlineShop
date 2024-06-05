using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем пользователей.
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
