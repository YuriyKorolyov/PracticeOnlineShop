namespace MyApp.Dto.Account
{
    /// <summary>
    /// DTO для представления данных нового пользователя.
    /// </summary>
    public class NewUserDto
    {
        /// <summary>
        /// Получает или задает имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Получает или задает адрес электронной почты пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Получает или задает токен аутентификации пользователя.
        /// </summary>
        public string Token { get; set; }
    }
}
