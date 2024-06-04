using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class Payment : IEntity  
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }        
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }

        public int OrderId { get; set; }
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
