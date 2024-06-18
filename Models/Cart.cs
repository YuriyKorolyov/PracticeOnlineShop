using MyApp.Models.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет сущность корзины.
    /// </summary>
    public class Cart : IEntity
    {
        /// <summary>
        /// Получает или задает идентификатор корзины.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает количество продуктов в корзине.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Получает или задает пользователя, которому принадлежит корзина.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Получает или задает продукт, который находится в корзине.
        /// </summary>
        public Product Product { get; set; }
    }
}
