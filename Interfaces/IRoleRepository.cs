using MyApp.Dto.Create;
using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем ролей.
    /// </summary>
    public interface IRoleRepository : IBaseRepository<Role>
    {
        /// <summary>
        /// Получает роли, обрезанные до верхнего регистра, на основе данных о создаваемой роли.
        /// </summary>
        /// <param name="roleCreate">Данные о создаваемой роли.</param>
        /// <returns>Роли, обрезанные до верхнего регистра.</returns>
        Task<Role> GetRolesTrimToUpperAsync(RoleCreateDto roleCreate);
    }
}
