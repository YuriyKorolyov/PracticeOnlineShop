using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    /// Контроллер для управления корзиной.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CartController"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work для управления транзакциями и сохранениями.</param>
        /// <param name="cartService">Сервис для работы с корзинами.</param>
        /// <param name="productService">Сервис для работы с продуктами.</param>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        /// <param name="mapper">Маппер для преобразования между DTO и сущностями.</param>
        public CartController(
            IUnitOfWork unitOfWork,
            ICartService cartService, 
            IProductService productService, 
            IUserService userService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
            _productService = productService;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает корзину пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список корзин пользователя.</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartReadDto>>> GetCartsByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            var carts = await _cartService.GetByUserId(userId)
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
        public async Task<ActionResult<CartReadDto>> AddToCartAsync([FromBody] CartCreateDto cartDto, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(cartDto.ProductId, cancellationToken);
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
            cart.User = await _userService.GetByIdAsync(cartDto.UserId, cancellationToken);

            await _cartService.AddAsync(cart, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            var createdCartDto = _mapper.Map<CartReadDto>(cart);
            return CreatedAtAction(nameof(GetCartsByUserIdAsync), new { userId = cartDto.UserId }, createdCartDto);
        }

        /// <summary>
        /// Удаляет продукт из корзины по его идентификатору.
        /// </summary>
        /// <param name="cartId">Идентификатор корзины.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> RemoveFromCartAsync(int cartId, CancellationToken cancellationToken)
        {
            await _cartService.DeleteByIdAsync(cartId, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Обновляет информацию в корзине.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCartAsync(int userId, CancellationToken cancellationToken)
        {
            await _cartService.DeleteByUserIdAsync(userId, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
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
        public async Task<IActionResult> UpdateCartAsync(int cartId, [FromBody] CartUpdateDto cartDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productService.GetByIdAsync(cartDto.ProductId, cancellationToken);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            if (product.StockQuantity < cartDto.Quantity)
            {
                return BadRequest($"Not enough stock for product {product.Name}. Available: {product.StockQuantity}, Requested: {cartDto.Quantity}");
            }

            var cart = await _cartService.GetByIdAsync(cartId, cancellationToken);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var user = await _userService.GetByIdAsync(cartDto.UserId, cancellationToken);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (cart.User.Id != cartDto.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            if (cart.Product.Id != cartDto.ProductId)
            {
                return BadRequest("Product ID mismatch.");
            }

            cart.Quantity = cartDto.Quantity;

            await _cartService.UpdateAsync(cart, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return NoContent();
        }
    }
}

