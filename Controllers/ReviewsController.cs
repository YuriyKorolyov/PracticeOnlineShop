using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public ReviewsController(IReviewRepository reviewRepository, IMapper mapper, IProductRepository productRepository, IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(await _reviewRepository.GetReviewsAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<ProductDto>> GetReview(int reviewId)
        {
            if (! await _reviewRepository.ReviewExistsAsync(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReviewByIdAsync(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("pokemon/{pokeId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForAProduct(int prodId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(await _reviewRepository.GetReviewsOfAProductAsync(prodId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview([FromQuery] int userId, [FromQuery] int prodId, [FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetReviewsTrimToUpperAsync(reviewDto);

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = _mapper.Map<Review>(reviewDto);

            review.Product = await _productRepository.GetProductByIdAsync(prodId);
            review.User = await _userRepository.GetUserByIdAsync(userId);

            if (! await _reviewRepository.CreateReviewAsync(review))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdReviewDto = _mapper.Map<ReviewDto>(review);
            return CreatedAtAction(nameof(GetReview), new { id = createdReviewDto.Id }, createdReviewDto);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewId != updatedReview.Id)
                return BadRequest(ModelState);

            if (! await _reviewRepository.ReviewExistsAsync(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (! await _reviewRepository.UpdateReviewAsync(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            if (! await _reviewRepository.ReviewExistsAsync(reviewId))
            {
                return NotFound();
            }

            var reviewToDelete = await _reviewRepository.GetReviewByIdAsync(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (! await _reviewRepository.DeleteReviewAsync(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
            }

            return NoContent();
        }

        [HttpDelete("/DeleteReviewsByUser/{reviewerId}")]
        public async Task<IActionResult> DeleteReviewsByReviewer(int userId)
        {
            if (! await _userRepository.UserExistsAsync(userId))
                return NotFound();

            var reviewsToDelete = await _userRepository.GetReviewsByUserAsync(userId);
            if (!ModelState.IsValid)
                return BadRequest();

            if (! await _reviewRepository.DeleteReviewsAsync(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "error deleting reviews");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }    
    }
}
