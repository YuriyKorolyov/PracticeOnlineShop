using MyApp.Dto.Read;

namespace MyApp.Dto.Update
{
    public class ViewHistoryUpdateDto
    {
        public int Id { get; set; }
        public DateTime ViewDate { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
