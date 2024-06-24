using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyApp.Configurations;
using MyApp.IServices;
using MyApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApp.Services
{
    /// <summary>
    /// Сервис для создания JWT токенов.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TokenService"/>.
        /// </summary>
        /// <param name="jwtSettings">Настройки JWT.</param>
        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        }

        /// <summary>
        /// Создает JWT токен для указанного пользователя и ролей.
        /// </summary>
        /// <param name="user">Пользователь, для которого создается токен.</param>
        /// <param name="roles">Список ролей пользователя.</param>
        /// <returns>Созданный JWT токен.</returns>
        public string CreateToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(_jwtSettings.TokenLifetimeDays),
                SigningCredentials = creds,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
