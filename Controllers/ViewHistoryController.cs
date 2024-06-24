using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto.Read;
using MyApp.Dto.Create;
using MyApp.Dto.Update;
using MyApp.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Repository.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Controllers
{
    /// <summary>
    /// Контроллер для управления историей просмотров.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ViewHistoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IViewHistoryService _viewHistoryService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ViewHistoriesController"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work для управления транзакциями и сохранениями.</param>
        /// <param name="viewHistoryService">Репозиторий для управления историей просмотров.</param>
        /// <param name="productService">Репозиторий для управления продуктами.</param>
        /// <param name="userService">Репозиторий для управления пользователями.</param>
        /// <param name="mapper">Интерфейс для отображения объектов.</param>
        public ViewHistoriesController(
            IUnitOfWork unitOfWork,
            IViewHistoryService viewHistoryService, 
            IProductService productService, 
            IUserService userService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _viewHistoryService = viewHistoryService;
            _userService = userService;
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает историю просмотров по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>История просмотров пользователя.</returns>
        [HttpGet]
        [Authorize(Policy = "RequireAdminRole, RequireUserRole")]
        public async Task<ActionResult<IEnumerable<ViewHistoryReadDto>>> GetViewHistoryByUserAsync([FromQuery] int userId, CancellationToken cancellationToken)
        {
            var viewHistory = await _viewHistoryService.GetByUserId(userId)
                .ProjectTo<ViewHistoryReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(viewHistory);
        }

        /// <summary>
        /// Получает историю просмотра по идентификатору.
        /// </summary>
        /// <param name="viewId">Идентификатор истории просмотра.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>История просмотра.</returns>
        [HttpGet("{viewId}")]
        [Authorize(Policy = "RequireAdminRole, RequireUserRole")]
        public async Task<ActionResult<ViewHistoryReadDto>> GetViewHistoryByIdAsync(int viewId, CancellationToken cancellationToken)
        {
            if (!await _viewHistoryService.ExistsAsync(viewId, cancellationToken))
                return NotFound();

            var review = _mapper.Map<ViewHistoryReadDto>(await _viewHistoryService.GetByIdAsync(viewId, query =>
            query.Include(vh => vh.Product),
            cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        /// <summary>
        /// Получает количество просмотров для определенного продукта.
        /// </summary>
        /// <param name="prodId">Идентификатор продукта.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Количество просмотров.</returns>
        [HttpGet("product/{prodId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<int>> GetCountVHForAProductAsync(int prodId, CancellationToken cancellationToken)
        {
            var views = await _viewHistoryService.GetCountViewHistoryOfAProductAsync(prodId, cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(views);
        }

        /// <summary>
        /// Создает новую запись об истории просмотра.
        /// </summary>
        /// <param name="viewDto">Данные новой записи об истории просмотра.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданная запись об истории просмотра.</returns>
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole, RequireUserRole")]
        public async Task<ActionResult> AddViewHistoryAsync([FromBody] ViewHistoryCreateDto viewDto, CancellationToken cancellationToken)
        {
            if (viewDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var view = _mapper.Map<ViewHistory>(viewDto);

            view.Product = await _productService.GetByIdAsync(viewDto.ProductId, cancellationToken);
            view.User = await _userService.GetByIdAsync(viewDto.UserId, cancellationToken);

            await _viewHistoryService.AddAsync(view, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            var createdViewDto = _mapper.Map<ViewHistoryReadDto>(view);
            return CreatedAtAction(nameof(GetViewHistoryByIdAsync), new { viewId = createdViewDto.Id }, createdViewDto);
        }

        /// <summary>
        /// Обновляет запись об истории просмотра.
        /// </summary>
        /// <param name="viewId">Идентификатор записи об истории просмотра.</param>
        /// <param name="updatedView">Обновленные данные записи об истории просмотра.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{viewId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateViewHistoryAsync(int viewId, [FromBody] ViewHistoryUpdateDto updatedView, CancellationToken cancellationToken)
        {
            if (updatedView == null)
                return BadRequest(ModelState);

            if (viewId != updatedView.Id)
                return BadRequest(ModelState);

            if (!await _viewHistoryService.ExistsAsync(viewId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var view = await _viewHistoryService.GetByIdAsync(viewId, cancellationToken);
            view.ViewDate = updatedView.ViewDate;

            await _viewHistoryService.UpdateAsync(view, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Удаляет историю просмотров пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("/DeleteViewHistoryByUser/{userId}")]
        [Authorize(Policy = "RequireAdminRole, RequireUserRole")]
        public async Task<IActionResult> DeleteViewHistoryByUserAsync(int userId, CancellationToken cancellationToken)
        {
            if (!await _userService.ExistsAsync(userId, cancellationToken))
                return NotFound();

            var viewsToDelete = await _viewHistoryService.GetByUserId(userId)
                .Select(vh => vh.Id)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest();

            await _viewHistoryService.DeleteByIdsAsync(viewsToDelete, cancellationToken);

            return NoContent();
        }
    }
}
