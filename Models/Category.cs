using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
