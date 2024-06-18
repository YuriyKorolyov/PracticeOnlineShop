using System.ComponentModel.DataAnnotations;

namespace MyApp.Dto.Account
{
    /// <summary>
    /// DTO для регистрации пользователя.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Получает или задает имя пользователя.
        /// </summary>
        /// <value>Имя пользователя.</value>
        [Required]
        public string? UserName { get; set; }

        /// <summary>
        /// Получает или задает адрес электронной почты.
        /// </summary>
        /// <value>Адрес электронной почты.</value>
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Получает или задает пароль.
        /// </summary>
        /// <value>Пароль.</value>  
        [Required]
        public string? Password { get; set; }

        /// <summary>
        /// Получает или задает номер телефона.
        /// </summary>
        /// <value>Номер телефона.</value>
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Получает или задает имя.
        /// </summary>
        /// <value>Имя.</value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Получает или задает фамилию.
        /// </summary>
        /// <value>Фамилия.</value>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Получает или задает адрес доставки.
        /// </summary>
        /// <value>Адрес доставки.</value>
        [Required]
        public string ShippingAddress { get; set; }
    }
}
