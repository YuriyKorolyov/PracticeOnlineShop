using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RoleRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateRoleAsync(Role role)
        {
            _context.Add(role);
            return await Save();
        }

        public async Task<bool> DeleteRoleAsync(Role role)
        {
            _context.Remove(role);
            return await Save();
        }

        public async Task<bool> DeleteRolesAsync(List<Role> roles)
        {
            _context.RemoveRange(roles);
            return await Save();
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.Where(r => r.Id == roleId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRolesTrimToUpperAsync(RoleDto roleCreate)
        {
            var roles = await GetRolesAsync();
            return roles.Where(c => c.RoleName.Trim().ToUpper() == roleCreate.RoleName.TrimEnd().ToUpper()).FirstOrDefault();
        }

        public async Task<bool> RoleExistsAsync(int roleId)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            _context.Update(role);
            return await Save();
        }
    }
}
