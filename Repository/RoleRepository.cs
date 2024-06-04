using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Role> GetRolesTrimToUpperAsync(RoleCreateDto roleCreate)
        {
            return await GetAll().Where(c => c.RoleName.Trim().ToUpper() == roleCreate.RoleName.TrimEnd().ToUpper()).FirstOrDefaultAsync();
        }
    }
}
