using Microsoft.EntityFrameworkCore;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с промокодами.
    /// </summary>
    /// <typeparam name="PromoCode">Тип сущности промокода.</typeparam>
    public class PromoCodeService : BaseService<PromoCode>, IPromoCodeService
    {
        public PromoCodeService(IBaseRepository<PromoCode> repository) : base(repository)
        {
        }

        /// <summary>
        /// Получает промокод по его имени.
        /// </summary>
        /// <param name="promoName">Имя промокода.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Промо-код, соответствующий указанному имени.</returns>
        public async Task<PromoCode> GetByNameAsync(string promoName, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .Where(r => r.PromoName == promoName)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
