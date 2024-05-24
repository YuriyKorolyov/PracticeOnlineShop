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
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }



        //[HttpPost]
        //public async Task<ActionResult<Cart>> PostCart(Cart cart)
        //{
        //    var product = await _context.Products.FindAsync(cart.ProductId);
        //    if (product == null)
        //    {
        //        return BadRequest($"Product with ID {cart.ProductId} not found.");
        //    }

        //    if (product.StockQuantity < cart.Quantity)
        //    {
        //        return BadRequest($"Not enough stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {cart.Quantity}");
        //    }

        //    _context.Carts.Add(cart);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetCart), new { id = cart.CartId }, cart);
        //}

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetCartsByUserId(int userId)
        {
            var carts = await _cartRepository.GetCartsByUserIdAsync(userId);
            var cartDtos = _mapper.Map<IEnumerable<CartDto>>(carts);
            return Ok(cartDtos);
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> AddToCart(int UserId, CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _cartRepository.AddToCartAsync(cart);

            var createdCartDto = _mapper.Map<CartDto>(cart);
            return CreatedAtAction(nameof(GetCartsByUserId), new { userId = UserId }, createdCartDto);
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int cartId)
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
        public async Task<IActionResult> UpdateCart([FromQuery] int userId, [FromQuery] int productId, [FromBody] CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            await _cartRepository.UpdateCartAsync(cart);

            return NoContent();
        }
    }
}

