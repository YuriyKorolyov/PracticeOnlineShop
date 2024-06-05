using MyApp.Dto.Create;
using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IQueryable<Product> GetProductsByCategory(int categoryId);
        Task<Category> GetCategoryTrimToUpperAsync(CategoryCreateDto categoryCreate, CancellationToken cancellationToken = default);
    }
}
