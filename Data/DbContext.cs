using Microsoft.EntityFrameworkCore;
using MyApp.Configurations;
using MyApp.Models;

namespace MyApp.Data
{
    /// <summary>
    /// Представляет контекст базы данных для приложения.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="options">Параметры для настройки контекста базы данных.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Получает или задает набор данных для таблицы пользователей.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы продуктов.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы категорий.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы заказов.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы деталей заказов.
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы отзывов.
        /// </summary>
        public DbSet<Review> Reviews { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы платежей.
        /// </summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы промокодов.
        /// </summary>
        public DbSet<PromoCode> PromoCodes { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы истории просмотров.
        /// </summary>
        public DbSet<ViewHistory> ViewHistories { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы корзины.
        /// </summary>
        public DbSet<Cart> Carts { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы ПродуктКатегория.
        /// </summary>
        public DbSet<ProductCategory> ProductCategories { get; set; }

        /// <summary>
        /// Получает или задает набор данных для таблицы ролей.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Применяет конфигурации к модели при ее создании.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели контекста базы данных.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Применение конфигураций сущностей
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PromoCodeConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ViewHistoryConfiguration());            
        }
    }

}
