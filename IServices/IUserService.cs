using MyApp.Models;
using MyApp.IServices.BASE;

namespace MyApp.IServices
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем пользователей.
    /// </summary>
    public interface IUserService : IBaseService<User>
    {
    }
}
