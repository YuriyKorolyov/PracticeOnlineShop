using MyApp.Models;

namespace MyApp.Dto.ReadDto
{
    public class PaymentReadDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public int? PromoId { get; set; }
        public string PromoName { get; set; }

        public OrderReadDto Order { get; set; }
    }
}
