using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetByName(string productName, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .Where(p => p.Name == productName)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<decimal> GetProductRating(int id, CancellationToken cancellationToken = default)
        {
            var reviews = await _context.Reviews
                                        .Where(p => p.Product.Id == id)
                                        .ToListAsync(cancellationToken);

            if (!reviews.Any()) 
                return 0;

            return Math.Round((decimal)reviews.Sum(r => r.Rating) / reviews.Count(), 2);
        }
        
        public async Task<Product> GetProductTrimToUpperAsync(ProductCreateDto productCreate, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
