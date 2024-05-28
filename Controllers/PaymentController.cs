using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto.ReadDto;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentReadDto>>> GetPayments()
        {
            var payments = await _paymentRepository.GetPaymentsAsync();
            var paymentDtos = _mapper.Map<IEnumerable<PaymentReadDto>>(payments);
            return Ok(paymentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentReadDto>> GetPayment(int id)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            var paymentDto = _mapper.Map<PaymentReadDto>(payment);
            return Ok(paymentDto);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentReadDto>> AddPayment(PaymentReadDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = _mapper.Map<Payment>(paymentDto);
            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = (PaymentStatus)new Random().Next(0, 3);

            await _paymentRepository.AddPaymentAsync(payment);

            var createdPaymentDto = _mapper.Map<PaymentReadDto>(payment);
            return CreatedAtAction(nameof(GetPayment), new { id = createdPaymentDto.Id }, createdPaymentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentReadDto paymentDto)
        {
            if (id != paymentDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var payment = _mapper.Map<Payment>(paymentDto);
            await _paymentRepository.UpdatePaymentAsync(payment);
            return NoContent();
        }

        [HttpPost("{id}/apply-promo/{promoId}")]
        public async Task<IActionResult> ApplyPromoCode(int id, int promoId)
        {
            try
            {
                await _paymentRepository.ApplyPromoCodeAsync(id, promoId);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }
    }
}

