using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .AsQueryable();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetProductAsync(string productName)
        {
            return await _context.Products.Where(p => p.Name == productName).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetProductRatingAsync(int id)
        {
            var reviews = await _context.Reviews
                                        .Where(p => p.Product.Id == id)
                                        .ToListAsync();

            if (!reviews.Any()) 
                return 0;

            return Math.Round((decimal)reviews.Sum(r => r.Rating) / reviews.Count(), 2);
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            _context.Add(product);

            return await Save();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Update(product);
            return await Save();
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            _context.Remove(product);
            return await Save();
        }

        public async Task<bool> ProductExistsAsync(int prodId)
        {
            return await _context.Products.AnyAsync(p => p.Id == prodId);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
        public async Task<Product> GetProductTrimToUpperAsync(ProductCreateDto productCreate)
        {
            return await _context.Products.Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper()).FirstOrDefaultAsync();
        }
    }
}
