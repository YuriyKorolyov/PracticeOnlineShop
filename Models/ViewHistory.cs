using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class ViewHistory : IEntity
    {
        public int Id { get; set; }
        public DateTime ViewDate { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }

}
