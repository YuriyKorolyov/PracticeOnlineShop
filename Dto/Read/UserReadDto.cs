namespace MyApp.Dto.Read
{
    /// <summary>
    /// Представляет модель данных для чтения информации о пользователе.
    /// </summary>
    public class UserReadDto
    {
        /// <summary>
        /// Получает или задает уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

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
        /// Получает или задает хеш пароля пользователя.
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
    }
}
