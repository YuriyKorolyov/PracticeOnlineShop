namespace MyApp.Dto.Update
{
    public class ReviewUpdateDto
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
