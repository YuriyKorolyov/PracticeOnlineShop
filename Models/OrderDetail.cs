using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class OrderDetail : IEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
