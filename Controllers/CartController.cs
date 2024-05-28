using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Dto;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetCartsByUserId(int userId)
        {
            var carts = await _cartRepository.GetCartsByUserIdAsync(userId);
            var cartDtos = _mapper.Map<IEnumerable<CartDto>>(carts);
            return Ok(cartDtos);
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> AddToCart([FromQuery] int userId, [FromQuery] int prodId, [FromBody] CartDto cartDto)
        {
            var product = await _productRepository.GetProductByIdAsync(prodId);
            if (product == null)
            {
                return NotFound(); 
            }

            if (product.StockQuantity < cartDto.Quantity)
            {
                return BadRequest($"Not enough stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {cartDto.Quantity}");
            }
            var cart = _mapper.Map<Cart>(cartDto);
            cart.Product = product;
            cart.User = await _userRepository.GetUserByIdAsync(userId);

            await _cartRepository.AddToCartAsync(cart);

            var createdCartDto = _mapper.Map<CartDto>(cart);
            return CreatedAtAction(nameof(GetCartsByUserId), new { userId = userId }, createdCartDto);
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId, int cartId)
        {
            await _cartRepository.RemoveFromCartAsync(cartId);
            return NoContent();
        }

        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartRepository.ClearCartAsync(userId);
            return NoContent();
        }

        [HttpPut("{userId}/{productId}")]
        public async Task<IActionResult> UpdateCart(int userId, int productId, [FromBody] CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _cartRepository.UpdateCartAsync(cart);

            return NoContent();
        }
    }
}

