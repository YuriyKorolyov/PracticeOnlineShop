using MyApp.Dto;
using MyApp.Dto.Create;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetCategories();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<bool> CategoryExistsAsync(int id);
        Task<bool> CreateCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(Category category);
        Task<bool> Save();
        Task<Category> GetCategoryTrimToUpperAsync(CategoryCreateDto categoryCreate);
    }
}
