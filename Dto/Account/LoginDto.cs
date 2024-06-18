using System.ComponentModel.DataAnnotations;

namespace MyApp.Dto.Account
{
    /// <summary>
    /// DTO для выполнения входа в систему.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Получает или задает имя пользователя.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Получает или задает пароль пользователя.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
