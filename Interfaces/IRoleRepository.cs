using MyApp.Dto;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IRoleRepository
    {
        Task<ICollection<Role>> GetRolesAsync();
        Task<Role> GetRoleByIdAsync(int roleId);
        Task<bool> RoleExistsAsync(int roleId);
        Task<bool> CreateRoleAsync(Role role);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(Role role);
        Task<bool> DeleteRolesAsync(List<Role> roles);
        Task<bool> Save();
        Task<Role> GetRolesTrimToUpperAsync(RoleDto roleCreate);
    }
}
