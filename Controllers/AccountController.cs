using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Dto.Account;
using MyApp.IServices;
using MyApp.Models;

namespace MyApp.Controllers
{
    /// <summary>
    /// Контроллер для обработки операций учетной записи пользователя, таких как вход и регистрация.
    /// </summary>
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signinManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AccountController"/>.
        /// </summary>
        /// <param name="userManager">Менеджер пользователей для управления пользователями.</param>
        /// <param name="tokenService">Сервис для создания аутентификационных токенов.</param>
        /// <param name="signInManager">Менеджер для управления операциями входа в систему пользователей.</param>
        /// <param name="mapper">Маппер для преобразования DTO в доменные модели.</param>
        public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Обрабатывает запрос на вход пользователя.
        /// </summary>
        /// <param name="loginDto">DTO с данными для входа (имя пользователя и пароль).</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции входа (успешно или неуспешно).</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username, cancellationToken);

            if (user == null) 
                return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) 
                return Unauthorized("Username not found and/or password incorrect");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user, roles)
                }
            );
        }

        /// <summary>
        /// Обрабатывает запрос на регистрацию нового пользователя.
        /// </summary>
        /// <param name="registerDto">DTO с данными для регистрации нового пользователя.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат операции регистрации (успешно или с ошибкой).</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = _mapper.Map<User>(registerDto);

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(appUser);
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser, roles)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
