using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto;
using MyApp.Dto.Create;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            _context.Add(category);
            return await Save();
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            _context.Remove(category);
            return await Save();
        }

        public IQueryable<Category> GetCategories()
        {
            return _context.Categories.AsQueryable();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Categories
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.ProductCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Product).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            _context.Update(category);
            return await Save();
        }

        public async Task<Category> GetCategoryTrimToUpperAsync(CategoryCreateDto categoryCreate)
        {
            return _context.Categories.Where(c => c.CategoryName.Trim().ToUpper() == categoryCreate.CategoryName.TrimEnd().ToUpper()).FirstOrDefault();
        }
    }
}
