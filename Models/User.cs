using MyApp.Repository.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет пользователя.
    /// </summary>
    public class User : IEntity
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
        /// Получает или задает дату регистрации пользователя.
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Получает или задает заказы, созданные данным пользователем.
        /// </summary>
        public ICollection<Order> Orders { get; set; }

        /// <summary>
        /// Получает или задает отзывы, оставленные данным пользователем.
        /// </summary>
        public ICollection<Review> Reviews { get; set; }

        /// <summary>
        /// Получает или задает историю просмотров, связанную с данным пользователем.
        /// </summary>
        public ICollection<ViewHistory> ViewHistories { get; set; }

        /// <summary>
        /// Получает или задает корзины покупок, связанные с данным пользователем.
        /// </summary>
        public ICollection<Cart> Carts { get; set; }

        /// <summary>
        /// Получает или задает роль данного пользователя.
        /// </summary>
        public Role Role { get; set; }
    }

}
