namespace MyApp.Dto.Update
{
    /// <summary>
    /// Представляет модель данных для обновления пользователя.
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// Получает или задает идентификатор пользователя.
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
        /// Получает или задает идентификатор роли пользователя.
        /// </summary>
        public int RoleId { get; set; }
    }
}
