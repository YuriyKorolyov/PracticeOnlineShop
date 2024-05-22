using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto;
using MyApp.Interfaces;
using MyApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            var payments = await _paymentRepository.GetPaymentsAsync();
            var paymentDtos = _mapper.Map<IEnumerable<PaymentDto>>(payments);
            return Ok(paymentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            var paymentDto = _mapper.Map<PaymentDto>(payment);
            return Ok(paymentDto);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> AddPayment(PaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = _mapper.Map<Payment>(paymentDto);
            await _paymentRepository.AddPaymentAsync(payment);

            var createdPaymentDto = _mapper.Map<PaymentDto>(payment);
            return CreatedAtAction(nameof(GetPayment), new { id = createdPaymentDto.Id }, createdPaymentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentDto paymentDto)
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

