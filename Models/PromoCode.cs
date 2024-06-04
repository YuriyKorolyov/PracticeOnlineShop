using MyApp.Interfaces.BASE;

namespace MyApp.Models
{
    public class PromoCode : IEntity
    {
        public int Id { get; set; }
        public string PromoName { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }

}
