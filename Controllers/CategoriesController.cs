using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository;

namespace MyApp.Controllers
{
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
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(await _categoryRepository.GetCategoriesAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int categoryId)
        {
            if (! await _categoryRepository.CategoryExistsAsync(categoryId))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(await _categoryRepository.GetCategoryByIdAsync(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("product/{categoryId}")]
        public async Task<ActionResult<int>> GetProductByCategoryId(int categoryId)
        {
            var products = _mapper.Map<List<ProductDto>>(await _categoryRepository.GetProductsByCategoryAsync(categoryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = await _categoryRepository.GetCategoryTrimToUpperAsync(categoryCreate);

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (! await _categoryRepository.CreateCategoryAsync(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdCategoryDto = _mapper.Map<CategoryDto>(categoryMap);
            return CreatedAtAction(nameof(GetCategory), new { categoryId = createdCategoryDto.Id }, createdCategoryDto);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategory.Id)
                return BadRequest(ModelState);

            if (! await _categoryRepository.CategoryExistsAsync(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Category>(updatedCategory);

            if (! await _categoryRepository.UpdateCategoryAsync(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (! await _categoryRepository.CategoryExistsAsync(categoryId))
            {
                return NotFound();
            }

            var categoryToDelete = await _categoryRepository.GetCategoryByIdAsync(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (! await _categoryRepository.DeleteCategoryAsync(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }

    }
}
