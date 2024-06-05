using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с ролями пользователей.
    /// </summary>
    /// <typeparam name="Role">Тип сущности роли.</typeparam>
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RoleRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получает роль по наименованию с учетом регистра.
        /// </summary>
        /// <param name="roleCreate">DTO для создания роли.</param>
        /// <returns>Роль пользователя.</returns>
        public async Task<Role> GetRolesTrimToUpperAsync(RoleCreateDto roleCreate)
        {
            return await GetAll().Where(c => c.RoleName.Trim().ToUpper() == roleCreate.RoleName.TrimEnd().ToUpper()).FirstOrDefaultAsync();
        }
    }
}
