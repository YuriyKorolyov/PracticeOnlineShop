﻿using AutoMapper;
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
    /// Контроллер для управления платежами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly IPromoCodeService _promoCodeService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PaymentController"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work для управления транзакциями и сохранениями.</param>
        /// <param name="paymentService">Репозиторий для работы с платежами.</param>
        /// <param name="orderService">Репозиторий для работы с заказами.</param>
        /// <param name="promoCodeService">Репозиторий для работы с промокодами.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public PaymentController(
            IUnitOfWork unitOfWork,
            IPaymentService paymentService, 
            IOrderService orderService, 
            IPromoCodeService promoCodeService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            _orderService = orderService;
            _promoCodeService = promoCodeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех платежей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список платежей.</returns>
        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<IEnumerable<PaymentReadDto>>> GetPaymentsAsync(CancellationToken cancellationToken)
        {
            var payments = await _paymentService.GetAll()
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
        public async Task<ActionResult<PaymentReadDto>> GetPaymentByIdAsync(int payId, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.GetByIdAsync(payId, query =>
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
        public async Task<ActionResult<PaymentReadDto>> AddPaymentAsync(PaymentCreateDto paymentDto, CancellationToken cancellationToken)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            payment.Order = await _orderService.GetByIdAsync(paymentDto.OrderId, cancellationToken);

            if (!string.IsNullOrEmpty(paymentDto.PromoName))
            {
                var promoCode = await _promoCodeService.GetByNameAsync(paymentDto.PromoName, cancellationToken);
                if (promoCode != null && promoCode.EndDate > DateTime.UtcNow)
                {
                    payment.PromoCode = promoCode;
                    payment.Amount = payment.Order.TotalAmount * (1 - promoCode.Discount);
                }
            }        

            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = (PaymentStatus)new Random().Next(0, 3);

            await _paymentService.AddAsync(payment, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);


            var createdPaymentDto = _mapper.Map<PaymentReadDto>(payment);
            return CreatedAtAction(nameof(GetPaymentByIdAsync), new { payId = createdPaymentDto.Id }, createdPaymentDto);
        }

        /// <summary>
        /// Обновляет информацию о платеже.
        /// </summary>
        /// <param name="payId">Идентификатор платежа, который нужно обновить.</param>
        /// <param name="paymentDto">Данные для обновления платежа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{payId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdatePaymentAsync(int payId, PaymentUpdateDto paymentDto, CancellationToken cancellationToken)
        {
            if (payId != paymentDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var payment = await _paymentService.GetByIdAsync(payId, cancellationToken);

            var order = await _orderService.GetByIdAsync(paymentDto.OrderId, cancellationToken);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            payment.Order = order;
            payment.Amount = payment.Order.TotalAmount;

            if (!string.IsNullOrEmpty(paymentDto.PromoName))
            {
                var promoCode = await _promoCodeService.GetByNameAsync(paymentDto.PromoName, cancellationToken);
                if (promoCode != null && promoCode.EndDate > DateTime.UtcNow)
                {
                    payment.PromoCode = promoCode;
                    payment.Amount -= payment.Order.TotalAmount * promoCode.Discount;
                }                
            }

            payment.Status = paymentDto.Status;

            await _paymentService.UpdateAsync(payment, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return NoContent();
        }
    }
}

