using MyApp.Models;

namespace MyApp.Dto.Create
{
    public class ViewHistoryCreateDto
    {
        public DateTime ViewDate { get; set; }

        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
