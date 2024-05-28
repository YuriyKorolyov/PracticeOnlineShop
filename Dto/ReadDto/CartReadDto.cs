using MyApp.Models;

namespace MyApp.Dto.ReadDto
{
    public class CartReadDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductReadDto Product { get; set; }
    }
}
