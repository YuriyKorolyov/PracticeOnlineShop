using MyApp.Dto;
using MyApp.Dto.Create;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IRoleRepository
    {
        IQueryable<Role> GetRoles();
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<bool> RoleExistsAsync(int roleId);
        Task<bool> CreateRoleAsync(Role role);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(Role role);
        Task<bool> DeleteRolesAsync(List<Role> roles);
        Task<bool> Save();
        Task<Role> GetRolesTrimToUpperAsync(RoleCreateDto roleCreate);
    }
}
