using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с промокодами.
    /// </summary>
    /// <typeparam name="PromoCode">Тип сущности промокода.</typeparam>
    public class PromoCodeRepository : BaseRepository<PromoCode>, IPromoCodeRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PromoCodeRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public PromoCodeRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Получает промокод по его имени.
        /// </summary>
        /// <param name="promoName">Имя промокода.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        /// <returns>Промо-код, соответствующий указанному имени.</returns>
        public async Task<PromoCode> GetByName(string promoName, CancellationToken cancellationToken = default)
        {
            return await GetAll()
                .Where(r => r.PromoName == promoName)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
