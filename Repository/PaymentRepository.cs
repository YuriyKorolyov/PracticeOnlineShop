using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Payment> GetPayments()
        {
            return _context.Payments
                .Include(p => p.PromoCode)
                .AsQueryable();
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments.Include(p => p.PromoCode).FirstOrDefaultAsync(p => p.Id == paymentId);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}
