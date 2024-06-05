using MyApp.Interfaces.BASE;
using MyApp.Models;

namespace MyApp.Interfaces
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем платежей.
    /// </summary>
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
    }
}
