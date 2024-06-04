using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class Cart : IEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }

}
