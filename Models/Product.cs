using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    /// <summary>
    /// Представляет сущность продукта.
    /// </summary>
    public class Product : IEntity
    {
        /// <summary>
        /// Получает или задает идентификатор продукта.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название продукта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает описание продукта.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Получает или задает цену продукта.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получает или задает количество продукта в наличии.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Получает или задает URL изображения продукта.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Получает или задает коллекцию деталей заказов, связанных с данным продуктом.
        /// </summary>
        public ICollection<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Получает или задает коллекцию отзывов, связанных с данным продуктом.
        /// </summary>
        public ICollection<Review> Reviews { get; set; }

        /// <summary>
        /// Получает или задает коллекцию истории просмотров, связанных с данным продуктом.
        /// </summary>
        public ICollection<ViewHistory> ViewHistories { get; set; }

        /// <summary>
        /// Получает или задает коллекцию корзин, связанных с данным продуктом.
        /// </summary>
        public ICollection<Cart> Carts { get; set; }

        /// <summary>
        /// Получает или задает коллекцию категорий, к которым относится данный продукт.
        /// </summary>
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
