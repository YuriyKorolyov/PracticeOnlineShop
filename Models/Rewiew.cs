using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }

}
