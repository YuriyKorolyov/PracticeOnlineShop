using MyApp.Models;

namespace MyApp.Dto.Read
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        public PaymentReadDto Payment { get; set; }
        public ICollection<OrderDetailReadDto> OrderDetails { get; set; }
    }
}
