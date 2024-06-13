using MyApp.IServices.BASE;
using MyApp.Models;

namespace MyApp.IServices
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем промокодов.
    /// </summary>
    public interface IPromoCodeService : IBaseService<PromoCode>
    {
        /// <summary>
        /// Получает промокод по его имени.
        /// </summary>
        /// <param name="promoName">Имя промокода.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Промокод.</returns>
        Task<PromoCode> GetByNameAsync(string promoName, CancellationToken cancellationToken = default);
    }
}
