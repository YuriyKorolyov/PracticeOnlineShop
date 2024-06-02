using MyApp.Dto.Create;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(string productName);
        Task<decimal> GetProductRatingAsync(int id);
        IQueryable<Product> GetProducts();
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> ProductExistsAsync(int id);
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Product product);
        Task<bool> Save();
        Task<Product> GetProductTrimToUpperAsync(ProductCreateDto productCreate);
    }
}
