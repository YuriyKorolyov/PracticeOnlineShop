using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var productDtos = _mapper.Map<List<ProductDto>>(await _productRepository.GetProductsAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productDtos);
        }

        [HttpGet("{prodId}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int prodId)
        {
            if (! await _productRepository.ProductExistsAsync(prodId))
                return NotFound();

            var product = _mapper.Map<ProductDto>(_productRepository.GetProductByIdAsync(prodId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }


        [HttpGet("{prodId}/rating")]
        public async Task<ActionResult<decimal>> GetProductRating(int prodId)
        {
            if (! await _productRepository.ProductExistsAsync(prodId))
                return NotFound();

            var rating = await _productRepository.GetProductRatingAsync(prodId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct([FromQuery] int catId, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest(ModelState);

            var product = _productRepository.GetProductTrimToUpperAsync(productDto);

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productDto);
            if (!await _productRepository.AddProductAsync(catId, productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdProductDto = _mapper.Map<ProductDto>(productMap);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProductDto.Id }, createdProductDto);
        }

        [HttpPut("{prodId}")]
        public async Task<IActionResult> PutProduct(int prodId, [FromQuery] int catId, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest(ModelState);

            if (prodId != productDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (! await _productRepository.ProductExistsAsync(prodId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var product = _mapper.Map<Product>(productDto);

            if (! await _productRepository.UpdateProductAsync(catId, product))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{prodId}")]
        public async Task<IActionResult> DeleteProduct(int prodId)
        {
            if (! await _productRepository.ProductExistsAsync(prodId))
            {
                return NotFound();
            }

            var reviewsToDelete = await _reviewRepository.GetReviewsOfAProductAsync(prodId);
            var productToDelete = await _productRepository.GetProductByIdAsync(prodId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (! await _reviewRepository.DeleteReviewsAsync(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (! await _productRepository.DeleteProductAsync(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
            }

            return NoContent();
        }       
    }
}
