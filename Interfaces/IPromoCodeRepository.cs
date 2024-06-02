using MyApp.Dto.Create;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IPromoCodeRepository
    {
        Task<PromoCode> GetPromoCodeByNameAsync(string promoName);
        IQueryable<PromoCode> GetPromoCodes();
        Task<PromoCode> GetPromoCodeByIdAsync(int promoId);
        Task<bool> PromoCodeExistsAsync(int promoId);
        Task<bool> CreatePromoCodeAsync(PromoCode promo);
        Task<bool> UpdatePromoCodeAsync(PromoCode promo);
        Task<bool> DeletePromoCodeAsync(PromoCode promo);
        Task<bool> SaveAsync();
    }
}
