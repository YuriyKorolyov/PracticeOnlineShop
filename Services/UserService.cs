using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с пользователями.
    /// </summary>
    /// <typeparam name="User">Тип сущности пользователя.</typeparam>
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IBaseRepository<User> repository) : base(repository)
        {
        }
    }
}
