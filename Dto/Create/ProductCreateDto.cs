using MyApp.Dto.Read;

namespace MyApp.Dto.Create
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
    }
}
