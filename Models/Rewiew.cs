using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class Review : IEntity
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }

}
