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
    /// Контроллер для управления платежами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PaymentController"/>.
        /// </summary>
        /// <param name="paymentRepository">Репозиторий для работы с платежами.</param>
        /// <param name="orderRepository">Репозиторий для работы с заказами.</param>
        /// <param name="promoCodeRepository">Репозиторий для работы с промокодами.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public PaymentController(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IPromoCodeRepository promoCodeRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _promoCodeRepository = promoCodeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех платежей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список платежей.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentReadDto>>> GetPayments(CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetAll()
                .Include(p => p.PromoCode)
                .ProjectTo<PaymentReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(payments);
        }

        /// <summary>
        /// Получает платеж по его идентификатору.
        /// </summary>
        /// <param name="payId">Идентификатор платежа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Платеж.</returns>
        [HttpGet("{payId}")]
        public async Task<ActionResult<PaymentReadDto>> GetPayment(int payId, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetById(payId, query =>
            query.Include(p => p.PromoCode),
            cancellationToken);

            if (payment == null)
            {
                return NotFound();
            }

            var paymentDto = _mapper.Map<PaymentReadDto>(payment);
            return Ok(paymentDto);
        }

        /// <summary>
        /// Добавляет новый платеж.
        /// </summary>
        /// <param name="paymentDto">Данные нового платежа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Добавленный платеж.</returns>
        [HttpPost]
        public async Task<ActionResult<PaymentReadDto>> AddPayment(PaymentCreateDto paymentDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = _mapper.Map<Payment>(paymentDto);
            payment.Order = await _orderRepository.GetById(paymentDto.OrderId, cancellationToken);

            if (!string.IsNullOrEmpty(paymentDto.PromoName))
            {
                var promoCode = await _promoCodeRepository.GetByName(paymentDto.PromoName, cancellationToken);
                if (promoCode != null && promoCode.EndDate > DateTime.UtcNow)
                {
                    payment.PromoCode = promoCode;
                    payment.Amount = payment.Order.TotalAmount * (1 - promoCode.Discount);
                }
            }        

            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = (PaymentStatus)new Random().Next(0, 3);

            await _paymentRepository.Add(payment, cancellationToken);

            var createdPaymentDto = _mapper.Map<PaymentReadDto>(payment);
            return CreatedAtAction(nameof(GetPayment), new { payId = createdPaymentDto.Id }, createdPaymentDto);
        }

        /// <summary>
        /// Обновляет информацию о платеже.
        /// </summary>
        /// <param name="payId">Идентификатор платежа, который нужно обновить.</param>
        /// <param name="paymentDto">Данные для обновления платежа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{payId}")]
        public async Task<IActionResult> UpdatePayment(int payId, PaymentUpdateDto paymentDto, CancellationToken cancellationToken)
        {
            if (payId != paymentDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var payment = await _paymentRepository.GetById(payId, cancellationToken);

            var order = await _orderRepository.GetById(paymentDto.OrderId, cancellationToken);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            payment.Order = order;
            payment.Amount = payment.Order.TotalAmount;

            if (!string.IsNullOrEmpty(paymentDto.PromoName))
            {
                var promoCode = await _promoCodeRepository.GetByName(paymentDto.PromoName, cancellationToken);
                if (promoCode != null && promoCode.EndDate > DateTime.UtcNow)
                {
                    payment.PromoCode = promoCode;
                    payment.Amount -= payment.Order.TotalAmount * promoCode.Discount;
                }                
            }

            payment.Status = paymentDto.Status;

            await _paymentRepository.Update(payment, cancellationToken);
            return NoContent();
        }
    }
}

