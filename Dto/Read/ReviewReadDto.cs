namespace MyApp.Dto.Read
{
    public class ReviewReadDto
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }

        public UserReadDto User { get; set; }
    }
}
