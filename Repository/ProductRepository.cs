using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto;
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

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductAsync(string productName)
        {
            return await _context.Products.Where(p => p.Name == productName).FirstOrDefaultAsync();
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

        public async Task<bool> AddProductAsync(int categoryId, Product product)
        {
            var category = await _context.Categories.Where(a => a.Id == categoryId).FirstOrDefaultAsync();
           
            var productCategory = new ProductCategory()
            {
                Category = category,
                Product = product,
            };

            _context.Add(productCategory);
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
        public async Task<Product> GetProductTrimToUpperAsync(ProductDto productCreate)
        {
            var products = await GetProductsAsync();
            return products.Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
        }
    }
}
