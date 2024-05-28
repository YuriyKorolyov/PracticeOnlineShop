using MyApp.Dto.ReadDto;
using MyApp.Models;

namespace MyApp.Dto.CreateDto
{
    public class PaymentCreateDto
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public int? PromoId { get; set; }
        public string PromoName { get; set; }
        public int OrderId { get; set; }
    }
}
