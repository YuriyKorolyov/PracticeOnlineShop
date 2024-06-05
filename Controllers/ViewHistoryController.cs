using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto.Read;
using MyApp.Dto.Create;
using MyApp.Dto.Update;
using MyApp.Interfaces;
using MyApp.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<ViewHistoryReadDto>>> GetViewHistoryByUser([FromQuery] int userId, CancellationToken cancellationToken)
        {
            var viewHistory = await _viewHistoryRepository.GetByUserId(userId)
                .ProjectTo<ViewHistoryReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(viewHistory);
        }

        [HttpGet("{viewId}")]
        public async Task<ActionResult<ViewHistoryReadDto>> GetViewHistory(int viewId, CancellationToken cancellationToken)
        {
            if (!await _viewHistoryRepository.Exists(viewId, cancellationToken))
                return NotFound();

            var review = _mapper.Map<ViewHistoryReadDto>(await _viewHistoryRepository.GetById(viewId, query =>
            query.Include(vh => vh.Product),
            cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("product/{prodId}")]
        public async Task<ActionResult<int>> GetCountVHForAProduct(int prodId, CancellationToken cancellationToken)
        {
            var views = await _viewHistoryRepository.GetCountViewHistoryOfAProduct(prodId, cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(views);
        }

        [HttpPost]
        public async Task<ActionResult> CreateViewHistory([FromBody] ViewHistoryCreateDto viewDto, CancellationToken cancellationToken)
        {
            if (viewDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var view = _mapper.Map<ViewHistory>(viewDto);

            view.Product = await _productRepository.GetById(viewDto.ProductId, cancellationToken);
            view.User = await _userRepository.GetById(viewDto.UserId, cancellationToken);

            if (!await _viewHistoryRepository.Add(view, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdViewDto = _mapper.Map<ViewHistoryReadDto>(view);
            return CreatedAtAction(nameof(GetViewHistory), new { viewId = createdViewDto.Id }, createdViewDto);
        }

        [HttpPut("{viewId}")]
        public async Task<IActionResult> UpdateViewHistory(int viewId, [FromBody] ViewHistoryUpdateDto updatedView, CancellationToken cancellationToken)
        {
            if (updatedView == null)
                return BadRequest(ModelState);

            if (viewId != updatedView.Id)
                return BadRequest(ModelState);

            if (!await _viewHistoryRepository.Exists(viewId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var view = await _viewHistoryRepository.GetById(viewId, cancellationToken);
            view.ViewDate = updatedView.ViewDate;            

            if (!await _viewHistoryRepository.Update(view, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong updating viewhistory");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("/DeleteViewHistoryByUser/{userId}")]
        public async Task<IActionResult> DeleteViewHistoryByUser(int userId, CancellationToken cancellationToken)
        {
            if (!await _userRepository.Exists(userId, cancellationToken))
                return NotFound();

            var viewsToDelete = await _viewHistoryRepository.GetByUserId(userId)
                .Select(vh => vh.Id)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _viewHistoryRepository.DeleteByIds(viewsToDelete, cancellationToken))
            {
                ModelState.AddModelError("", "error deleting reviews");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
