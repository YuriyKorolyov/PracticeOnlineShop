using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;

namespace MyApp.Repository
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly ApplicationDbContext _context;
        public PromoCodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreatePromoCodeAsync(PromoCode promo)
        {
            _context.Add(promo);
            return await SaveAsync();
        }

        public async Task<bool> DeletePromoCodeAsync(PromoCode promo)
        {
            _context.Remove(promo);
            return await SaveAsync();
        }

        public async Task<PromoCode> GetPromoCodeByIdAsync(int promoId)
        {
            return await _context.PromoCodes.Where(r => r.Id == promoId).FirstOrDefaultAsync();
        }

        public async Task<PromoCode> GetPromoCodeByNameAsync(string promoName)
        {
            return await _context.PromoCodes.Where(r => r.PromoName == promoName).FirstOrDefaultAsync();
        }

        public IQueryable<PromoCode> GetPromoCodes()
        {
            return _context.PromoCodes.AsQueryable();
        }
        public async Task<bool> PromoCodeExistsAsync(int promoId)
        {
            return await _context.PromoCodes.AnyAsync(r => r.Id == promoId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdatePromoCodeAsync(PromoCode promo)
        {
            _context.Update(promo);
            return await SaveAsync();
        }
    }
}
