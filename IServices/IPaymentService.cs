using MyApp.IServices.BASE;
using MyApp.Models;

namespace MyApp.IServices
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем платежей.
    /// </summary>
    public interface IPaymentService : IBaseService<Payment>
    {
    }
}
