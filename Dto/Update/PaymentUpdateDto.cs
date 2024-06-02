using MyApp.Dto.Read;
using MyApp.Models;

namespace MyApp.Dto.Update
{
    public class PaymentUpdateDto
    {
        public int Id { get; set; }
        public PaymentStatus Status { get; set; }
        public string PromoName { get; set; }

        public int OrderId { get; set; }
    }
}
