using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Dto.Update;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.UnitOfWorks;

namespace MyApp.Controllers
{
    /// <summary>
    /// Контроллер для управления продуктами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ProductsController"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work для управления транзакциями и сохранениями.</param>
        /// <param name="productService">Репозиторий для работы с продуктами.</param>
        /// <param name="reviewService">Репозиторий для работы с отзывами.</param>
        /// <param name="categoryService">Репозиторий для работы с категориями.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public ProductsController(
            IUnitOfWork unitOfWork,
            IProductService productService, 
            IReviewService reviewService, 
            ICategoryService categoryService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _reviewService = reviewService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех продуктов.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список продуктов.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var productDtos = await _productService.GetAll()
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productDtos);
        }

        /// <summary>
        /// Получает рейтинг продукта по его идентификатору.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Рейтинг продукта.</returns>
        [HttpGet("{prodId}/rating")]
        [AllowAnonymous]
        public async Task<ActionResult<decimal>> GetProductRatingAsync(int prodId, CancellationToken cancellationToken)
        {
            if (! await _productService.ExistsAsync(prodId, cancellationToken))
                return NotFound();

            var rating = await _productService.GetProductRatingAsync(prodId, cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        /// <summary>
        /// Получает информацию о продукте по его идентификатору.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о продукте.</returns>
        [HttpGet("{prodId}")]
        [Authorize]
        public async Task<ActionResult<ProductReadDto>> GetProductByIdAsync(int prodId, CancellationToken cancellationToken)
        {
            if (!await _productService.ExistsAsync(prodId, cancellationToken))
                return NotFound();

            var product = _mapper.Map<ProductReadDto>(await _productService.GetByIdAsync(prodId, query =>
            query.Include(p => p.ProductCategories)
                 .ThenInclude(pc => pc.Category),
                 cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        /// <summary>
        /// Добавляет новый продукт.
        /// </summary>
        /// <param name="productDto">Данные нового продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Добавленный продукт.</returns>
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<ProductReadDto>> AddProductAsync([FromBody] ProductCreateDto productDto, CancellationToken cancellationToken)
        {
            if (productDto == null)
                return BadRequest(ModelState);

            if (await _productService.ExistsByNameAsync(productDto.Name, cancellationToken))
            {
                ModelState.AddModelError("", "Product already exists ");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productDto);

            var categories = await _categoryService.GetByIdsAsync(productDto.CategoryIds, cancellationToken);

            productMap.ProductCategories = categories.Select(category => new ProductCategory
            {
                Product = productMap,
                Category = category
            }).ToList();

            await _productService.AddAsync(productMap, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            var createdProductDto = _mapper.Map<ProductReadDto>(productMap);
            return CreatedAtAction(nameof(GetProductByIdAsync), new { prodId = createdProductDto.Id }, createdProductDto);
        }

        /// <summary>
        /// Обновляет информацию о продукте.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта, который нужно обновить.</param>
        /// <param name="productDto">Данные для обновления продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{prodId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateProductAsync(int prodId, [FromBody] ProductUpdateDto productDto, CancellationToken cancellationToken)
        {
            if (productDto == null)
                return BadRequest(ModelState);

            if (prodId != productDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (! await _productService.ExistsAsync(prodId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var product = await _productService.GetByIdAsync(prodId, cancellationToken);


            await _productService.UpdateAsync(product, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Удаляет продукт по его идентификатору.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{prodId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteProductAsync(int prodId, CancellationToken cancellationToken)
        {
            if (! await _productService.ExistsAsync(prodId, cancellationToken))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.DeleteByIdAsync(prodId, cancellationToken);

            return NoContent();
        }       
    }
}
