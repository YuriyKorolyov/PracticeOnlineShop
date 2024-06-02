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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IPromoCodeRepository promoCodeRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _promoCodeRepository = promoCodeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentReadDto>>> GetPayments()
        {
            var payments = await _paymentRepository.GetPayments()
                .ProjectTo<PaymentReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(payments);
        }

        [HttpGet("{payId}")]
        public async Task<ActionResult<PaymentReadDto>> GetPayment(int payId)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(payId);
            if (payment == null)
            {
                return NotFound();
            }
            var paymentDto = _mapper.Map<PaymentReadDto>(payment);
            return Ok(paymentDto);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentReadDto>> AddPayment(PaymentCreateDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = _mapper.Map<Payment>(paymentDto);
            payment.Order = await _orderRepository.GetOrderByIdAsync(paymentDto.OrderId);

            if (!string.IsNullOrEmpty(paymentDto.PromoName))
            {
                var promoCode = await _promoCodeRepository.GetPromoCodeByNameAsync(paymentDto.PromoName);
                if (promoCode != null && promoCode.EndDate > DateTime.UtcNow)
                {
                    payment.PromoCode = promoCode;
                    payment.Amount = payment.Order.TotalAmount * (1 - promoCode.Discount);
                }
            }        

            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = (PaymentStatus)new Random().Next(0, 3);

            await _paymentRepository.AddPaymentAsync(payment);

            var createdPaymentDto = _mapper.Map<PaymentReadDto>(payment);
            return CreatedAtAction(nameof(GetPayment), new { id = createdPaymentDto.Id }, createdPaymentDto);
        }

        [HttpPut("{payId}")]
        public async Task<IActionResult> UpdatePayment(int payId, PaymentUpdateDto paymentDto)
        {
            if (payId != paymentDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var payment = await _paymentRepository.GetPaymentByIdAsync(payId);

            var order = await _orderRepository.GetOrderByIdAsync(paymentDto.OrderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            payment.Order = order;
            payment.Amount = payment.Order.TotalAmount;

            if (!string.IsNullOrEmpty(paymentDto.PromoName))
            {
                var promoCode = await _promoCodeRepository.GetPromoCodeByNameAsync(paymentDto.PromoName);
                if (promoCode != null && promoCode.EndDate > DateTime.UtcNow)
                {
                    payment.PromoCode = promoCode;
                    payment.Amount -= payment.Order.TotalAmount * promoCode.Discount;
                }                
            }

            payment.Status = paymentDto.Status;

            await _paymentRepository.UpdatePaymentAsync(payment);
            return NoContent();
        }
    }
}

