using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.ProductCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Product).ToListAsync();
        }

        public async Task<Category> GetCategoryTrimToUpperAsync(CategoryCreateDto categoryCreate)
        {
            return await GetAll().Where(c => c.CategoryName.Trim().ToUpper() == categoryCreate.CategoryName.TrimEnd().ToUpper()).FirstOrDefaultAsync();
        }
    }
}
