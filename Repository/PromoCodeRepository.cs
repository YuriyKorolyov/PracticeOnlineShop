using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    public class PromoCodeRepository : BaseRepository<PromoCode>, IPromoCodeRepository
    {
        public PromoCodeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PromoCode> GetByName(string promoName, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .Where(r => r.PromoName == promoName)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
