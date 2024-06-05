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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public ReviewsController(IReviewRepository reviewRepository, IMapper mapper, IProductRepository productRepository, IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetReviews(CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetAll()
                .Include(r => r.User)
                .ProjectTo<ReviewReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<ReviewReadDto>> GetReview(int reviewId, CancellationToken cancellationToken)
        {
            if (! await _reviewRepository.Exists(reviewId, cancellationToken))
                return NotFound();

            var review = _mapper.Map<ReviewReadDto>(await _reviewRepository.GetById(reviewId, query =>
            query.Include(r => r.User),
            cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("product/{prodId}")]
        public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetReviewsForAProduct(int prodId, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetReviewsOfAProduct(prodId)
                .ProjectTo<ReviewReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview([FromBody] ReviewCreateDto reviewDto, CancellationToken cancellationToken)
        {
            if (reviewDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = _mapper.Map<Review>(reviewDto);

            review.Product = await _productRepository.GetById(reviewDto.ProductId, cancellationToken);
            review.User = await _userRepository.GetById(reviewDto.UserId, cancellationToken);

            if (! await _reviewRepository.Add(review, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdReviewDto = _mapper.Map<ReviewReadDto>(review);
            return CreatedAtAction(nameof(GetReview), new { reviewId = createdReviewDto.Id }, createdReviewDto);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewUpdateDto updatedReview, CancellationToken cancellationToken)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewId != updatedReview.Id)
                return BadRequest(ModelState);

            if (! await _reviewRepository.Exists(reviewId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var review = await _reviewRepository.GetById(reviewId, cancellationToken);

            review.Product = await _productRepository.GetById(updatedReview.ProductId, cancellationToken);
            review.User = await _userRepository.GetById(updatedReview.UserId, cancellationToken);
            review.ReviewText = updatedReview.ReviewText;

            if (! await _reviewRepository.Update(review, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId, CancellationToken cancellationToken)
        {
            if (! await _reviewRepository.Exists(reviewId, cancellationToken))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _reviewRepository.DeleteById(reviewId, cancellationToken)) 
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
            }

            return NoContent();
        }

        [HttpDelete("/DeleteReviewsByUser/{userId}")]
        public async Task<IActionResult> DeleteReviewsByUser(int userId, CancellationToken cancellationToken)
        {
            if (!await _userRepository.Exists(userId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _reviewRepository.DeleteByUserId(userId, cancellationToken))
            {
                ModelState.AddModelError("", "error deleting reviews");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
