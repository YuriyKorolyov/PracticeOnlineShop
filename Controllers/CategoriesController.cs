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
    /// Контроллер для управления категориями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CategoriesController"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work для управления транзакциями и сохранениями.</param>
        /// <param name="categoryService">Сервис для работы с категориями.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public CategoriesController(
            IUnitOfWork unitOfWork,
            ICategoryService categoryService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех категорий.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список категорий.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAll()
                .ProjectTo<CategoryReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        /// <summary>
        /// Получает категорию по ее идентификатору.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Категория.</returns>
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            if (! await _categoryService.ExistsAsync(categoryId, cancellationToken))
                return NotFound();

            var category = _mapper.Map<CategoryReadDto>(await _categoryService.GetByIdAsync(categoryId, cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        /// <summary>
        /// Получает продукты по идентификатору категории.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список продуктов.</returns>
        [HttpGet("product/{categoryId}")]
        public async Task<ActionResult<int>> GetProductByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            var products = await _categoryService.GetProductsByCategory(categoryId)
                .ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(products);
        }

        /// <summary>
        /// Создает новую категорию.
        /// </summary>
        /// <param name="categoryCreate">Данные для создания категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Созданная категория.</returns>
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<CategoryReadDto>> AddCategoryAsync([FromBody] CategoryCreateDto categoryCreate, CancellationToken cancellationToken)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            if (await _categoryService.ExistsByNameAsync(categoryCreate.CategoryName, cancellationToken))
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            await _categoryService.AddAsync(categoryMap, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            var createdCategoryDto = _mapper.Map<CategoryReadDto>(categoryMap);
            return CreatedAtAction(nameof(GetCategoryByIdAsync), new { categoryId = createdCategoryDto.Id }, createdCategoryDto);
        }

        /// <summary>
        /// Обновляет существующую категорию.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="updatedCategory">Данные для обновления категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{categoryId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateCategoryAsync(int categoryId, [FromBody] CategoryUpdateDto updatedCategory, CancellationToken cancellationToken)
        {
            if (updatedCategory == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategory.Id)
                return BadRequest(ModelState);

            if (! await _categoryService.ExistsAsync(categoryId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = await _categoryService.GetByIdAsync(categoryId, cancellationToken);
            categoryMap.CategoryName = updatedCategory.CategoryName;

            await _categoryService.UpdateAsync(categoryMap, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Удаляет категорию по ее идентификатору.
        /// </summary>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{categoryId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            if (! await _categoryService.ExistsAsync(categoryId, cancellationToken))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _categoryService.DeleteByIdAsync(categoryId, cancellationToken);

            return NoContent();
        }

    }
}
