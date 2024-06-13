using MyApp.Models;
using MyApp.IServices.BASE;

namespace MyApp.IServices
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем ролей.
    /// </summary>
    public interface IRoleService : IBaseService<Role>
    {
        /// <summary>
        /// Получает роли, обрезанные до верхнего регистра, на основе данных о создаваемой роли.
        /// </summary>
        /// <param name="roleName">Название роли.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит значение true, если роль существует, иначе false.</returns>
        Task<bool> ExistsByNameAsync(string roleName, CancellationToken cancellationToken = default);
    }
}
