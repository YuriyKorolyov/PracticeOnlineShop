using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Controllers
{
    /// <summary>
    /// Контроллер для управления продуктами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ProductsController"/>.
        /// </summary>
        /// <param name="productRepository">Репозиторий для работы с продуктами.</param>
        /// <param name="reviewRepository">Репозиторий для работы с отзывами.</param>
        /// <param name="categoryRepository">Репозиторий для работы с категориями.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public ProductsController(IProductRepository productRepository, IReviewRepository reviewRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех продуктов.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список продуктов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProducts(CancellationToken cancellationToken)
        {
            var productDtos = await _productRepository.GetAll()
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productDtos);
        }

        /// <summary>
        /// Получает информацию о продукте по его идентификатору.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о продукте.</returns>
        [HttpGet("{prodId}")]
        public async Task<ActionResult<ProductReadDto>> GetProduct(int prodId, CancellationToken cancellationToken)
        {
            if (! await _productRepository.Exists(prodId, cancellationToken))
                return NotFound();

            var product = _mapper.Map<ProductReadDto>(await _productRepository.GetById(prodId, query=>
            query.Include(p=>p.ProductCategories)
                 .ThenInclude(pc=>pc.Category),
                 cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        /// <summary>
        /// Получает рейтинг продукта по его идентификатору.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Рейтинг продукта.</returns>
        [HttpGet("{prodId}/rating")]
        public async Task<ActionResult<decimal>> GetProductRating(int prodId, CancellationToken cancellationToken)
        {
            if (! await _productRepository.Exists(prodId, cancellationToken))
                return NotFound();

            var rating = await _productRepository.GetProductRating(prodId, cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        /// <summary>
        /// Добавляет новый продукт.
        /// </summary>
        /// <param name="productDto">Данные нового продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Добавленный продукт.</returns>
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> PostProduct([FromBody] ProductCreateDto productDto, CancellationToken cancellationToken)
        {
            if (productDto == null)
                return BadRequest(ModelState);

            var product = await _productRepository.GetProductTrimToUpperAsync(productDto, cancellationToken);

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists ");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productDto);

            var categories = await _categoryRepository.GetByIds(productDto.CategoryIds, cancellationToken);

            productMap.ProductCategories = categories.Select(category => new ProductCategory
            { 
                Product = product, 
                Category = category
            }).ToList();

            if (!await _productRepository.Add(productMap, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdProductDto = _mapper.Map<ProductReadDto>(productMap);
            return CreatedAtAction(nameof(GetProduct), new { prodId = createdProductDto.Id }, createdProductDto);
        }

        /// <summary>
        /// Обновляет информацию о продукте.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта, который нужно обновить.</param>
        /// <param name="productDto">Данные для обновления продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{prodId}")]
        public async Task<IActionResult> PutProduct(int prodId, [FromBody] ProductUpdateDto productDto, CancellationToken cancellationToken)
        {
            if (productDto == null)
                return BadRequest(ModelState);

            if (prodId != productDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (! await _productRepository.Exists(prodId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var product = await _productRepository.GetById(prodId, cancellationToken);            
            

            if (! await _productRepository.Update(product, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Удаляет продукт по его идентификатору.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{prodId}")]
        public async Task<IActionResult> DeleteProduct(int prodId, CancellationToken cancellationToken)
        {
            if (! await _productRepository.Exists(prodId, cancellationToken))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (! await _productRepository.DeleteById(prodId, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
            }

            return NoContent();
        }       
    }
}
