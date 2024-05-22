using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task ApplyPromoCodeAsync(int paymentId, int promoId);
    }
}
