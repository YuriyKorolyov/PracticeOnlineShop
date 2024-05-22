namespace MyApp.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        // Коллекция пользователей
        public ICollection<User> Users { get; set; }
    }
}
