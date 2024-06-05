using MyApp.Dto.Create;
using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByName(string productName, CancellationToken cancellationToken = default);
        Task<decimal> GetProductRating(int id, CancellationToken cancellationToken = default);
        Task<Product> GetProductTrimToUpperAsync(ProductCreateDto productCreate, CancellationToken cancellationToken = default);
    }
}
