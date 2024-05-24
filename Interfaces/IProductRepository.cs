using MyApp.Dto;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(string productName);
        Task<decimal> GetProductRatingAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> ProductExistsAsync(int id);
        Task<bool> AddProductAsync(int categoryID, Product product);
        Task<bool> UpdateProductAsync(int catId, Product product);
        Task<bool> DeleteProductAsync(Product product);
        Task<bool> Save();
        Task<Product> GetProductTrimToUpperAsync(ProductDto productCreate);
    }
}
