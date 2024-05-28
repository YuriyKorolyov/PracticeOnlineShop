using MyApp.Models;

namespace MyApp.Dto.ReadDto
{
    public class OrderDetailReadDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public ProductReadDto Product { get; set; }
    }
}
