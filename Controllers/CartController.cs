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
    /// Контроллер для управления корзиной.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CartController"/>.
        /// </summary>
        /// <param name="cartRepository">Репозиторий для работы с корзиной.</param>
        /// <param name="productRepository">Репозиторий для работы с продуктами.</param>
        /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public CartController(ICartRepository cartRepository, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает корзину пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список корзин пользователя.</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartReadDto>>> GetCartsByUserId(int userId, CancellationToken cancellationToken)
        {
            var carts = await _cartRepository.GetByUserId(userId)
            .ProjectTo<CartReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
            return Ok(carts);
        }

        /// <summary>
        /// Добавляет продукт в корзину.
        /// </summary>
        /// <param name="cartDto">Данные для создания корзины.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Созданная корзина.</returns>
        [HttpPost]
        public async Task<ActionResult<CartReadDto>> AddToCart([FromBody] CartCreateDto cartDto, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(cartDto.ProductId, cancellationToken);
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
            cart.User = await _userRepository.GetById(cartDto.UserId, cancellationToken);

            await _cartRepository.Add(cart, cancellationToken);

            var createdCartDto = _mapper.Map<CartReadDto>(cart);
            return CreatedAtAction(nameof(GetCartsByUserId), new { userId = cartDto.UserId }, createdCartDto);
        }

        /// <summary>
        /// Удаляет продукт из корзины по его идентификатору.
        /// </summary>
        /// <param name="cartId">Идентификатор корзины.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> RemoveFromCart(int cartId, CancellationToken cancellationToken)
        {
            await _cartRepository.DeleteById(cartId, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Обновляет информацию в корзине.
        /// </summary>
        /// <param name="cartId">Идентификатор корзины.</param>
        /// <param name="cartDto">Данные для обновления корзины.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCart(int userId, CancellationToken cancellationToken)
        {
            await _cartRepository.DeleteByUserId(userId, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Обновляет информацию в корзине.
        /// </summary>
        /// <param name="cartId">Идентификатор корзины.</param>
        /// <param name="cartDto">Данные для обновления корзины.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{cartId}")]
        public async Task<IActionResult> UpdateCart(int cartId, [FromBody] CartUpdateDto cartDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productRepository.GetById(cartDto.ProductId, cancellationToken);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            if (product.StockQuantity < cartDto.Quantity)
            {
                return BadRequest($"Not enough stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {cartDto.Quantity}");
            }

            var cart = await _cartRepository.GetById(cartId, cancellationToken);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var user = await _userRepository.GetById(cartDto.UserId, cancellationToken);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (cart.User.Id != cartDto.UserId || cart.Product.Id != cartDto.ProductId)
            {
                return BadRequest("User ID or Product ID mismatch.");
            }

            cart.Quantity = cartDto.Quantity;

            await _cartRepository.Update(cart, cancellationToken);

            return NoContent();
        }
    }
}

