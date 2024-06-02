using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Dto.Read;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrders()
        {
            var orders = await _orderRepository.GetOrders()
                .ProjectTo<OrderReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderReadDto>> GetOrder(int orderId)
        {
            if (! await _orderRepository.OrderExistsAsync(orderId))
                return NotFound();

            var order = _mapper.Map<OrderReadDto>(await _orderRepository.GetOrderByIdAsync(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> PostOrder([FromQuery] int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = new Order();

            order.OrderDate = DateTime.UtcNow;
            order.User = await _userRepository.GetUserByIdAsync(userId);
            var cartItems = await _cartRepository.GetCartsByUserId(userId).ToListAsync();//товары в заказ добавляются из корзины пользователя

            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest("Cart is empty.");
            }

            var orderDetails = cartItems.Select(ci => new OrderDetail
            {
                Product = ci.Product,
                Quantity = ci.Quantity,
                UnitPrice = ci.Product.Price
            }).ToList();

            foreach (var orderDetail in orderDetails)//проверка товара на существование и наличие необходимого количества 
            {
                var product = await _productRepository.GetProductByIdAsync(orderDetail.Product.Id);
                if (product == null)
                {
                    return BadRequest($"Product with ID {orderDetail.Product.Id} not found.");
                }

                if (product.StockQuantity < orderDetail.Quantity)
                {
                    return BadRequest($"Not enough stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {orderDetail.Quantity}");
                }

                product.StockQuantity -= orderDetail.Quantity;//количество товара в наличии уменьшается на число товара, которое заказали
            }

            order.OrderDetails = orderDetails;
            order.TotalAmount = orderDetails.Sum(od => od.Quantity * od.UnitPrice);//итоговая сумма заказа без учета промокода
            order.Status = OrderStatus.Processing;//статус заказа в обработке

            if (! await _orderRepository.CreateOrderAsync(order))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            await _cartRepository.ClearCartAsync(order.User.Id);

            var createdOrderDto = _mapper.Map<OrderReadDto>(order);
            return CreatedAtAction(nameof(GetOrder), new { orderId = createdOrderDto.Id }, createdOrderDto);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            if (!await _orderRepository.OrderExistsAsync(orderId))
                return NotFound();

            var orderToDelete = await _orderRepository.GetOrderByIdAsync(orderId);//получаем заказ с деталями заказа и товарами

            if (!ModelState.IsValid)
                return BadRequest();

            foreach (var orderDetail in orderToDelete.OrderDetails)
            {
                var product = await _productRepository.GetProductByIdAsync(orderDetail.Product.Id);
                if (product != null)
                {
                    product.StockQuantity += orderDetail.Quantity;//возвращаем товар в продажу
                }
            }

            if (!await _orderRepository.DeleteOrderAsync(orderToDelete))
            {
                ModelState.AddModelError("", "error deleting order");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }

}
