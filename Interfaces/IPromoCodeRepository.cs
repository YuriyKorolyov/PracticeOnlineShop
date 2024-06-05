using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IPromoCodeRepository : IBaseRepository<PromoCode>
    {
        Task<PromoCode> GetByName(string promoName, CancellationToken cancellationToken = default);
    }
}
