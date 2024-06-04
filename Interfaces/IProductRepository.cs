using MyApp.Dto.Create;
using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByName(string productName);
        Task<decimal> GetProductRating(int id);
        Task<Product> GetProductTrimToUpperAsync(ProductCreateDto productCreate);
    }
}
