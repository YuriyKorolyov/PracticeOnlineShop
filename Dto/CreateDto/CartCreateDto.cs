using MyApp.Dto.ReadDto;

namespace MyApp.Dto.CreateDto
{
    public class CartCreateDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
