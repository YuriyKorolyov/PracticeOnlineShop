using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.Dto.Update;
using MyApp.Dto.Create;
using MyApp.Dto.Read;
using MyApp.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Repository.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Controllers
{
    /// <summary>
    /// Контроллер для управления пользователями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UsersController"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work для управления транзакциями и сохранениями.</param>
        /// <param name="userService">Репозиторий для управления пользователями.</param>
        /// <param name="mapper">Интерфейс для отображения объектов.</param>
        public UsersController(
            IUnitOfWork unitOfWork,
            IUserService userService, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список пользователей.</returns>
        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var userDtos = await _userService.GetAll()
                .ProjectTo<UserReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Ok(userDtos);
        }

        /// <summary>
        /// Получает пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Пользователь.</returns>
        [HttpGet("{userId}")]
        [Authorize]
        [Authorize(Policy = "RequireAdminRole, RequireUserRole")]
        public async Task<ActionResult<UserReadDto>> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            if (!await _userService.ExistsAsync(userId, cancellationToken))
                return NotFound();

            var userDto = _mapper.Map<UserReadDto>(await _userService.GetByIdAsync(userId, cancellationToken));

            return Ok(userDto);
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="userDto">Данные нового пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданный пользователь.</returns>
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<UserReadDto>> AddUserAsync([FromBody] UserCreateDto userDto, CancellationToken cancellationToken)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(userDto);

            await _userService.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            var createdUserDto = _mapper.Map<UserReadDto>(user);
            return CreatedAtAction(nameof(GetUserByIdAsync), new { userId = createdUserDto.Id }, createdUserDto);
        }

        /// <summary>
        /// Обновляет информацию о пользователе.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя для обновления.</param>
        /// <param name="userDto">Данные для обновления пользователя.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{userId}")]
        [Authorize(Policy = "RequireAdminRole, RequireUserRole")]
        public async Task<IActionResult> UpdateUserAsync(int userId, [FromBody] UserUpdateDto userDto, CancellationToken cancellationToken)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            if (userId != userDto.Id)
                return BadRequest(ModelState);

            if (!await _userService.ExistsAsync(userId, cancellationToken))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userService.GetByIdAsync(userId, cancellationToken);
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.ShippingAddress = userDto.ShippingAddress;

            await _userService.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя для удаления.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{userId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            if (!await _userService.ExistsAsync(userId, cancellationToken))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.DeleteByIdAsync(userId, cancellationToken);

            return NoContent();
        }
    }
}
