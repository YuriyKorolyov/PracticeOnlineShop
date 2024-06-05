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
    /// <summary>
    /// Контроллер для управления промокодами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodesController : ControllerBase
    {
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PromoCodesController"/>.
        /// </summary>
        /// <param name="promoCodeRepository">Репозиторий для работы с промокодами.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public PromoCodesController(IPromoCodeRepository promoCodeRepository, IMapper mapper)
        {
            _promoCodeRepository = promoCodeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех промокодов.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список промокодов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCodeReadDto>>> GetPromos(CancellationToken cancellationToken)
        {
            var promos = await _promoCodeRepository.GetAll()
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
        public async Task<ActionResult<PromoCodeReadDto>> GetPromo(int promoId, CancellationToken cancellationToken)
        {
            if (!await _promoCodeRepository.Exists(promoId, cancellationToken))
                return NotFound();

            var promo = _mapper.Map<PromoCodeReadDto>(await _promoCodeRepository.GetById(promoId, cancellationToken));

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
        public async Task<ActionResult> CreatePromo([FromBody] PromoCodeCreateDto promoDto, CancellationToken cancellationToken)
        {
            if (promoDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var promo = _mapper.Map<PromoCode>(promoDto);

            if (!await _promoCodeRepository.Add(promo, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdPromoDto = _mapper.Map<PromoCodeReadDto>(promo);
            return CreatedAtAction(nameof(GetPromo), new { promoId = createdPromoDto.Id }, createdPromoDto);
        }

        /// <summary>
        /// Обновляет информацию о промокоде.
        /// </summary>
        /// <param name="promoId">Идентификатор промокода, который нужно обновить.</param>
        /// <param name="updatedPromo">Данные для обновления промокода.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{promoId}")]
        public async Task<IActionResult> UpdatePromo(int promoId, [FromBody] PromoCodeUpdateDto updatedPromo, CancellationToken cancellationToken)
        {
            if (updatedPromo == null)
                return BadRequest(ModelState);

            if (promoId != updatedPromo.Id)
                return BadRequest(ModelState);

            if (!await _promoCodeRepository.Exists(promoId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var promo = await _promoCodeRepository.GetById(promoId, cancellationToken);
            promo.StartDate = updatedPromo.StartDate;
            promo.EndDate = updatedPromo.EndDate;
            promo.PromoName = updatedPromo.PromoName;

            if (!await _promoCodeRepository.Update(promo, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Удаляет промокод по его идентификатору.
        /// </summary>
        /// <param name="promoId">Идентификатор промокода.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{promoId}")]
        public async Task<IActionResult> DeletePromo(int promoId, CancellationToken cancellationToken)
        {
            if (!await _promoCodeRepository.Exists(promoId, cancellationToken))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _promoCodeRepository.DeleteById(promoId, cancellationToken))
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
            }

            return NoContent();
        }
    }
}
