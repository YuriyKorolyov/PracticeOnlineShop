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
    /// Контроллер для управления промокодами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    public class PromoCodesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromoCodeService _promoCodeService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PromoCodesController"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work для управления транзакциями и сохранениями.</param>
        /// <param name="promoCodeService">Репозиторий для работы с промокодами.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public PromoCodesController(
            IUnitOfWork unitOfWork,
            IPromoCodeService promoCodeService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _promoCodeService = promoCodeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех промокодов.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список промокодов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCodeReadDto>>> GetPromosAsync(CancellationToken cancellationToken)
        {
            var promos = await _promoCodeService.GetAll()
                .ProjectTo<PromoCodeReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(promos);
        }

        /// <summary>
        /// Получает информацию о промокоде по его идентификатору.
        /// </summary>
        /// <param name="promoId">Идентификатор промокода.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о промокоде.</returns>
        [HttpGet("{promoId}")]
        public async Task<ActionResult<PromoCodeReadDto>> GetPromoByIdAsync(int promoId, CancellationToken cancellationToken)
        {
            if (!await _promoCodeService.ExistsAsync(promoId, cancellationToken))
                return NotFound();

            var promo = _mapper.Map<PromoCodeReadDto>(await _promoCodeService.GetByIdAsync(promoId, cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(promo);
        }

        /// <summary>
        /// Создает новый промокод.
        /// </summary>
        /// <param name="promoDto">Данные нового промокода.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public async Task<ActionResult> AddPromoAsync([FromBody] PromoCodeCreateDto promoDto, CancellationToken cancellationToken)
        {
            if (promoDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var promo = _mapper.Map<PromoCode>(promoDto);

            await _promoCodeService.AddAsync(promo, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            var createdPromoDto = _mapper.Map<PromoCodeReadDto>(promo);
            return CreatedAtAction(nameof(GetPromoByIdAsync), new { promoId = createdPromoDto.Id }, createdPromoDto);
        }

        /// <summary>
        /// Обновляет информацию о промокоде.
        /// </summary>
        /// <param name="promoId">Идентификатор промокода, который нужно обновить.</param>
        /// <param name="updatedPromo">Данные для обновления промокода.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{promoId}")]
        public async Task<IActionResult> UpdatePromoAsync(int promoId, [FromBody] PromoCodeUpdateDto updatedPromo, CancellationToken cancellationToken)
        {
            if (updatedPromo == null)
                return BadRequest(ModelState);

            if (promoId != updatedPromo.Id)
                return BadRequest(ModelState);

            if (!await _promoCodeService.ExistsAsync(promoId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var promo = await _promoCodeService.GetByIdAsync(promoId, cancellationToken);
            promo.StartDate = updatedPromo.StartDate;
            promo.EndDate = updatedPromo.EndDate;
            promo.PromoName = updatedPromo.PromoName;

            await _promoCodeService.UpdateAsync(promo, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Удаляет промокод по его идентификатору.
        /// </summary>
        /// <param name="promoId">Идентификатор промокода.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{promoId}")]
        public async Task<IActionResult> DeletePromoAsync(int promoId, CancellationToken cancellationToken)
        {
            if (!await _promoCodeService.ExistsAsync(promoId, cancellationToken))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _promoCodeService.DeleteByIdAsync(promoId, cancellationToken);

            return NoContent();
        }
    }
}
