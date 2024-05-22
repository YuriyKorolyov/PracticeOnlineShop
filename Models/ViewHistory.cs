using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class ViewHistory
    {
        public int Id { get; set; }
        public DateTime ViewDate { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }

}
