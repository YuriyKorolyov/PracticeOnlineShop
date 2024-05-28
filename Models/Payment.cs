using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class Payment    
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }

        public User User { get; set; }
        public Order Order { get; set; }
        public int? PromoId { get; set; }
        public PromoCode PromoCode { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Success,
        Failed
    }

}
