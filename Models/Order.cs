using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        public User User { get; set; }
        public Payment Payment { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public enum OrderStatus
    {
        Processing,
        Shipped,
        Completed
    }
}
