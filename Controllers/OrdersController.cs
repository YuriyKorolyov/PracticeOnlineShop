using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Dto.Read;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.UnitOfWorks;

namespace MyApp.Controllers
{
    /// <summary>
    /// Контроллер для управления заказами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrdersController"/>.
        /// </summary>
        /// <param name="orderService">Репозиторий для работы с заказами.</param>
        /// <param name="cartService">Репозиторий для работы с корзиной.</param>
        /// <param name="productService">Репозиторий для работы с продуктами.</param>
        /// <param name="userService">Репозиторий для работы с пользователями.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public OrdersController(
            IUnitOfWork unitOfWork, 
            IOrderService orderService, 
            ICartService cartService, 
            IProductService productService, 
            IUserService userService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _cartService = cartService;
            _productService = productService;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех заказов.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список заказов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAll()
                .Include(o => o.Payment)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ProjectTo<OrderReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        /// <summary>
        /// Получает заказ по его идентификатору.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Заказ.</returns>
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderReadDto>> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            if (! await _orderService.ExistsAsync(orderId, cancellationToken))
                return NotFound();

            var order = _mapper.Map<OrderReadDto>(await _orderService.GetByIdAsync(orderId, query =>
            query.Include(o => o.Payment)
                 .Include(o => o.OrderDetails)
                 .ThenInclude(od => od.Product)
                 .ThenInclude(p => p.ProductCategories)
                 .ThenInclude(pc => pc.Category),
                 cancellationToken));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> CreateOrderAsync([FromQuery] int userId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var user = await _userService.GetByIdAsync(userId, cancellationToken);

                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                var cartItems = await _cartService.GetByUserId(userId).ToListAsync(cancellationToken);//товары в заказ добавляются из корзины пользователя

                if (cartItems == null || cartItems.Count == 0)
                {
                    return BadRequest("Cart is empty.");
                }
               
                var orderDetails = new List<OrderDetail>();
                foreach (var cartItem in cartItems)
                {
                    var product = cartItem.Product;
                    if (product == null || product.StockQuantity < cartItem.Quantity)
                    {
                        return BadRequest($"Product {product?.Name ?? cartItem.Product.Id.ToString()} is not available.");
                    }

                    product.StockQuantity -= cartItem.Quantity;

                    var orderDetail = new OrderDetail(
                        cartItem.Quantity,
                        product.Price,
                        product);

                    orderDetails.Add(orderDetail);
                }

                decimal TotalAmount = orderDetails.Sum(od => od.Quantity * od.UnitPrice);

                var order = new Order
                (
                    TotalAmount,
                    OrderStatus.Processing,
                    user,
                    orderDetails
                );
                
                await _orderService.AddAsync(order, cancellationToken);
                await _cartService.DeleteByUserIdAsync(userId, cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                var createdOrderDto = _mapper.Map<OrderReadDto>(order);
                return CreatedAtAction(nameof(GetOrderByIdAsync), new { orderId = createdOrderDto.Id }, createdOrderDto);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Удаляет заказ по его идентификатору.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                if (! await _orderService.ExistsAsync(orderId, cancellationToken))
                    return NotFound();

                var orderToDelete = await _orderService.GetByIdAsync(orderId, query =>
                query.Include(o => o.OrderDetails)
                     .ThenInclude(od => od.Product),
                     cancellationToken);//получаем заказ с деталями заказа и товарами

                if (!ModelState.IsValid)
                    return BadRequest();

                foreach (var orderDetail in orderToDelete.OrderDetails)
                {
                    var product = orderDetail.Product;
                    if (product != null)
                    {
                        product.StockQuantity += orderDetail.Quantity;//возвращаем товар в продажу
                    }
                }

                await _orderService.DeleteByIdAsync(orderId, cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return StatusCode(500, "An error occurred while deleting the order.");
            }         
        }
    }

}
