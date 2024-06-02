using MyApp.Models;

namespace MyApp.Dto.Read
{
    public class PaymentReadDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public PromoCodeReadDto PromoCode { get; set; }

        public int OrderId { get; set; }
    }
}
