using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с ролями пользователей.
    /// </summary>
    /// <typeparam name="Role">Тип сущности роли.</typeparam>
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IBaseRepository<Role> repository) : base(repository)
        {
        }

        /// <summary>
        /// Получает роль по наименованию с учетом регистра.
        /// </summary>
        /// <param name="roleName">Название роли.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит значение true, если продукт существует, иначе false.</returns>
        public async Task<bool> ExistsByNameAsync(string roleName, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .AnyAsync(c => c.RoleName.Trim().ToUpper() == roleName.TrimEnd().ToUpper(), cancellationToken);
        }
    }
}
