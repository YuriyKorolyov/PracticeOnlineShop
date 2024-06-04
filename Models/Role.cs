using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
