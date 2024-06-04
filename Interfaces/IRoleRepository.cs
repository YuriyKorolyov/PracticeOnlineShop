using MyApp.Dto;
using MyApp.Dto.Create;
using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetRolesTrimToUpperAsync(RoleCreateDto roleCreate);
    }
}
