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
    public class PromoCodesController : ControllerBase
    {
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly IMapper _mapper;

        public PromoCodesController(IPromoCodeRepository promoCodeRepository, IMapper mapper)
        {
            _promoCodeRepository = promoCodeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCodeReadDto>>> GetPromos()
        {
            var promos = await _promoCodeRepository.GetPromoCodes()
                .ProjectTo<PromoCodeReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(promos);
        }

        [HttpGet("{promoId}")]
        public async Task<ActionResult<PromoCodeReadDto>> GetPromo(int promoId)
        {
            if (!await _promoCodeRepository.PromoCodeExistsAsync(promoId))
                return NotFound();

            var promo = _mapper.Map<PromoCodeReadDto>(await _promoCodeRepository.GetPromoCodeByIdAsync(promoId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(promo);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePromo([FromBody] PromoCodeCreateDto promoDto)
        {
            if (promoDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var promo = _mapper.Map<PromoCode>(promoDto);

            if (!await _promoCodeRepository.CreatePromoCodeAsync(promo))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var createdPromoDto = _mapper.Map<PromoCodeReadDto>(promo);
            return CreatedAtAction(nameof(GetPromo), new { promoId = createdPromoDto.Id }, createdPromoDto);
        }

        [HttpPut("{promoId}")]
        public async Task<IActionResult> UpdatePromo(int promoId, [FromBody] PromoCodeUpdateDto updatedPromo)
        {
            if (updatedPromo == null)
                return BadRequest(ModelState);

            if (promoId != updatedPromo.Id)
                return BadRequest(ModelState);

            if (!await _promoCodeRepository.PromoCodeExistsAsync(promoId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var promo = await _promoCodeRepository.GetPromoCodeByIdAsync(promoId);
            promo.StartDate = updatedPromo.StartDate;
            promo.EndDate = updatedPromo.EndDate;
            promo.PromoName = updatedPromo.PromoName;

            if (!await _promoCodeRepository.UpdatePromoCodeAsync(promo))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{promoId}")]
        public async Task<IActionResult> DeletePromo(int promoId)
        {
            if (!await _promoCodeRepository.PromoCodeExistsAsync(promoId))
            {
                return NotFound();
            }

            var promoToDelete = await _promoCodeRepository.GetPromoCodeByIdAsync(promoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _promoCodeRepository.DeletePromoCodeAsync(promoToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
            }

            return NoContent();
        }
    }
}
