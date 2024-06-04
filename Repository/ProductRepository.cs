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

        public async Task<Product> GetByName(string productName)
        {
            return await GetAll().Where(p => p.Name == productName).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetProductRating(int id)
        {
            var reviews = await _context.Reviews
                                        .Where(p => p.Product.Id == id)
                                        .ToListAsync();

            if (!reviews.Any()) 
                return 0;

            return Math.Round((decimal)reviews.Sum(r => r.Rating) / reviews.Count(), 2);
        }
        
        public async Task<Product> GetProductTrimToUpperAsync(ProductCreateDto productCreate)
        {
            return await GetAll().Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper()).FirstOrDefaultAsync();
        }
    }
}
