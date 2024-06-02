using MyApp.Models;

namespace MyApp.Dto.Create
{
    public class ReviewCreateDto
    {
        public string ReviewText { get; set; }
        public int Rating { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
