using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(string productName);
        Task<decimal> GetProductRatingAsync(int id);
        Task<bool> ProductExistsAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
