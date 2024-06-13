using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    /// Контроллер для управления отзывами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ReviewsController"/>.
        /// </summary>
        /// <param name="reviewService">Репозиторий для управления отзывами.</param>
        /// <param name="mapper">Интерфейс для отображения объектов.</param>
        /// <param name="productService">Репозиторий для управления продуктами.</param>
        /// <param name="userService">Репозиторий для управления пользователями.</param>
        public ReviewsController(
            IUnitOfWork unitOfWork,
            IReviewService reviewService, 
            IMapper mapper, 
            IProductService productService, 
            IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _reviewService = reviewService;
            _userService = userService;
            _productService = productService;
            _mapper = mapper;            
        }

        /// <summary>
        /// Получает все отзывы.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список отзывов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetReviewsAsync(CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetAll()
                .Include(r => r.User)
                .ProjectTo<ReviewReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        /// <summary>
        /// Получает отзыв по его идентификатору.
        /// </summary>
        /// <param name="reviewId">Идентификатор отзыва.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Отзыв.</returns>
        [HttpGet("{reviewId}")]
        public async Task<ActionResult<ReviewReadDto>> GetReviewByIdAsync(int reviewId, CancellationToken cancellationToken)
        {
            if (! await _reviewService.ExistsAsync(reviewId, cancellationToken))
                return NotFound();

            var review = _mapper.Map<ReviewReadDto>(await _reviewService.GetByIdAsync(reviewId, query =>
            query.Include(r => r.User),
            cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        /// <summary>
        /// Получает отзывы для продукта.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Отзывы для продукта.</returns>
        [HttpGet("product/{prodId}")]
        public async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetReviewsForAProductAsync(int prodId, CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetReviewsOfAProduct(prodId)
                .ProjectTo<ReviewReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        /// <summary>
        /// Создает новый отзыв.
        /// </summary>
        /// <param name="reviewDto">Данные отзыва.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданный отзыв.</returns>
        [HttpPost]
        public async Task<ActionResult> AddReviewAsync([FromBody] ReviewCreateDto reviewDto, CancellationToken cancellationToken)
        {
            if (reviewDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = _mapper.Map<Review>(reviewDto);

            review.Product = await _productService.GetByIdAsync(reviewDto.ProductId, cancellationToken);
            review.User = await _userService.GetByIdAsync(reviewDto.UserId, cancellationToken);

            await _reviewService.AddAsync(review, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            var createdReviewDto = _mapper.Map<ReviewReadDto>(review);
            return CreatedAtAction(nameof(GetReviewByIdAsync), new { reviewId = createdReviewDto.Id }, createdReviewDto);
        }

        /// <summary>
        /// Обновляет отзыв.
        /// </summary>
        /// <param name="reviewId">Идентификатор отзыва для обновления.</param>
        /// <param name="updatedReview">Обновленные данные отзыва.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Обновленный отзыв.</returns>
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReviewAsync(int reviewId, [FromBody] ReviewUpdateDto updatedReview, CancellationToken cancellationToken)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewId != updatedReview.Id)
                return BadRequest(ModelState);

            if (! await _reviewService.ExistsAsync(reviewId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var review = await _reviewService.GetByIdAsync(reviewId, cancellationToken);

            review.Product = await _productService.GetByIdAsync(updatedReview.ProductId, cancellationToken);
            review.User = await _userService.GetByIdAsync(updatedReview.UserId, cancellationToken);
            review.ReviewText = updatedReview.ReviewText;

            await _reviewService.UpdateAsync(review, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Удаляет отзыв.
        /// </summary>
        /// <param name="reviewId">Идентификатор отзыва для удаления.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId, CancellationToken cancellationToken)
        {
            if (! await _reviewService.ExistsAsync(reviewId, cancellationToken))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reviewService.DeleteByIdAsync(reviewId, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Удаляет отзывы конкретного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("/DeleteReviewsByUser/{userId}")]
        public async Task<IActionResult> DeleteReviewsByUserAsync(int userId, CancellationToken cancellationToken)
        {
            if (!await _userService.ExistsAsync(userId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            await _reviewService.DeleteByUserIdAsync(userId, cancellationToken);

            return NoContent();
        }
    }
}
