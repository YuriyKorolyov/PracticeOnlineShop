namespace MyApp.Dto.Create
{
    /// <summary>
    /// Представляет модель данных для создания нового пользователя.
    /// </summary>
    public class UserCreateDto
    {
        /// <summary>
        /// Получает или задает имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Получает или задает фамилию пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Получает или задает адрес электронной почты пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Получает или задает хэш пароля пользователя.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Получает или задает адрес доставки пользователя.
        /// </summary>
        public string ShippingAddress { get; set; }

        /// <summary>
        /// Получает или задает номер телефона пользователя.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Получает или задает дату регистрации пользователя.
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Получает или задает идентификатор роли пользователя.
        /// </summary>
        public int RoleId { get; set; }
    }
}
