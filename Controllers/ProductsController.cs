using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Dto;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository;
using System.Threading;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IReviewRepository reviewRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProducts()
        {
            var productDtos = await _productRepository.GetAll()
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productDtos);
        }

        [HttpGet("{prodId}")]
        public async Task<ActionResult<ProductReadDto>> GetProduct(int prodId)
        {
            if (! await _productRepository.Exists(prodId))
                return NotFound();

            var product = _mapper.Map<ProductReadDto>(await _productRepository.GetById(prodId, query=>
            query.Include(p=>p.ProductCategories)
                 .ThenInclude(pc=>pc.Category)));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }


        [HttpGet("{prodId}/rating")]
        public async Task<ActionResult<decimal>> GetProductRating(int prodId)
        {
            if (! await _productRepository.Exists(prodId))
                return NotFound();

            var rating = await _productRepository.GetProductRating(prodId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> PostProduct([FromBody] ProductCreateDto productDto)
        {
            if (productDto == null)
                return BadRequest(ModelState);

            var product = await _productRepository.GetProductTrimToUpperAsync(productDto);

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists ");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productDto);

            var categories = await _categoryRepository.GetByIds(productDto.CategoryIds);

            productMap.ProductCategories = categories.Select(category => new ProductCategory
            {
                Product = productMap,
                Category = category
            }).ToList();

            if (!await _productRepository.Add(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdProductDto = _mapper.Map<ProductReadDto>(productMap);
            return CreatedAtAction(nameof(GetProduct), new { prodId = createdProductDto.Id }, createdProductDto);
        }

        [HttpPut("{prodId}")]
        public async Task<IActionResult> PutProduct(int prodId, [FromBody] ProductUpdateDto productDto)
        {
            if (productDto == null)
                return BadRequest(ModelState);

            if (prodId != productDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (! await _productRepository.Exists(prodId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var product = await _productRepository.GetById(prodId);            
            

            if (! await _productRepository.Update(product))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{prodId}")]
        public async Task<IActionResult> DeleteProduct(int prodId)
        {
            if (! await _productRepository.Exists(prodId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (! await _productRepository.DeleteById(prodId))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
            }

            return NoContent();
        }       
    }
}
