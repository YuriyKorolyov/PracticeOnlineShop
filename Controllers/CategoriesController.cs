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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll()
                .ProjectTo<CategoryReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }
            
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategory(int categoryId, CancellationToken cancellationToken)
        {
            if (! await _categoryRepository.Exists(categoryId, cancellationToken))
                return NotFound();

            var category = _mapper.Map<CategoryReadDto>(await _categoryRepository.GetById(categoryId, cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("product/{categoryId}")]
        public async Task<ActionResult<int>> GetProductByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            var products = await _categoryRepository.GetProductsByCategory(categoryId)
                .ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> CreateCategory([FromBody] CategoryCreateDto categoryCreate, CancellationToken cancellationToken)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = await _categoryRepository.GetCategoryTrimToUpperAsync(categoryCreate, cancellationToken);

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (! await _categoryRepository.Add(categoryMap, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdCategoryDto = _mapper.Map<CategoryReadDto>(categoryMap);
            return CreatedAtAction(nameof(GetCategory), new { categoryId = createdCategoryDto.Id }, createdCategoryDto);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryUpdateDto updatedCategory, CancellationToken cancellationToken)
        {
            if (updatedCategory == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategory.Id)
                return BadRequest(ModelState);

            if (! await _categoryRepository.Exists(categoryId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = await _categoryRepository.GetById(categoryId, cancellationToken);
            categoryMap.CategoryName = updatedCategory.CategoryName;

            if (! await _categoryRepository.Update(categoryMap, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId, CancellationToken cancellationToken)
        {
            if (! await _categoryRepository.Exists(categoryId, cancellationToken))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (! await _categoryRepository.DeleteById(categoryId, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

    }
}
