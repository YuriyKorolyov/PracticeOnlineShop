using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IPaymentRepository
    {
        IQueryable<Payment> GetPayments();
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
    }
}
