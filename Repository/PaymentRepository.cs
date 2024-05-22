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

        public async Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            return await _context.Payments.Include(p => p.PromoCode).ToListAsync();
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

        public async Task ApplyPromoCodeAsync(int paymentId, int promoId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
            {
                throw new InvalidOperationException("Payment not found.");
            }

            var promoCode = await _context.PromoCodes.FindAsync(promoId);
            if (promoCode == null)
            {
                throw new InvalidOperationException("Promo code not found.");
            }

            payment.PromoId = promoId;
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}
