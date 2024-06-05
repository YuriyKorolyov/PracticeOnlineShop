using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем промокодов.
    /// </summary>
    public interface IPromoCodeRepository : IBaseRepository<PromoCode>
    {
        /// <summary>
        /// Получает промокод по его имени.
        /// </summary>
        /// <param name="promoName">Имя промокода.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Промокод.</returns>
        Task<PromoCode> GetByName(string promoName, CancellationToken cancellationToken = default);
    }
}
