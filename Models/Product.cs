using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ViewHistory> ViewHistories { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}