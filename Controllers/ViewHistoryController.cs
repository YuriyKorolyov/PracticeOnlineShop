using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewHistoriesController : ControllerBase
    {
        private readonly IViewHistoryRepository _viewHistoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ViewHistoriesController(IViewHistoryRepository viewHistoryRepository, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _viewHistoryRepository = viewHistoryRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewHistoryDto>>> GetViewHistoryByUser([FromQuery] int userId)
        {
            var viewHistory = _mapper.Map<List<ViewHistoryDto>>(await _viewHistoryRepository.GetViewHistoryByUserIdAsync(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(viewHistory);
        }

        [HttpGet("{viewId}")]
        public async Task<ActionResult<ProductDto>> GetViewHistory(int viewId)
        {
            if (!await _viewHistoryRepository.ViewHistoryExistsAsync(viewId))
                return NotFound();

            var review = _mapper.Map<ViewHistoryDto>(await _viewHistoryRepository.GetViewHistoryByIdAsync(viewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("product/{prodId}")]
        public async Task<ActionResult<int>> GetCountVHForAProduct(int prodId)
        {
            var views = await _viewHistoryRepository.GetCountViewHistoryOfAProductAsync(prodId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(views);
        }

        [HttpPost]
        public async Task<ActionResult> CreateViewHistory([FromQuery] int userId, [FromQuery] int prodId, [FromBody] ViewHistoryDto viewDto)
        {
            if (viewDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var view = _mapper.Map<ViewHistory>(viewDto);

            view.Product = await _productRepository.GetProductByIdAsync(prodId);
            view.User = await _userRepository.GetUserByIdAsync(userId);

            if (!await _viewHistoryRepository.CreateViewHistoryAsync(view))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdViewDto = _mapper.Map<ViewHistoryDto>(view);
            return CreatedAtAction(nameof(GetViewHistory), new { viewId = createdViewDto.Id }, createdViewDto);
        }

        [HttpPut("{viewId}")]
        public async Task<IActionResult> UpdateReview(int viewId, [FromBody] ViewHistoryDto updatedView)
        {
            if (updatedView == null)
                return BadRequest(ModelState);

            if (viewId != updatedView.Id)
                return BadRequest(ModelState);

            if (!await _viewHistoryRepository.ViewHistoryExistsAsync(viewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var viewMap = _mapper.Map<ViewHistory>(updatedView);

            if (!await _viewHistoryRepository.UpdateViewHistoryAsync(viewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("/DeleteViewHistoryByUser/{userId}")]
        public async Task<IActionResult> DeleteViewHistoryByUser(int userId)
        {
            if (!await _userRepository.UserExistsAsync(userId))
                return NotFound();

            var viewsToDelete = await _viewHistoryRepository.GetViewHistoryByUserIdAsync(userId);
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _viewHistoryRepository.DeleteViewHistoryAsync(viewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "error deleting reviews");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
