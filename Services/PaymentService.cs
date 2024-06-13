
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.BASE;
using MyApp.Services.BASE;

namespace MyApp.Services
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с платежами.
    /// </summary>
    /// <typeparam name="Payment">Тип сущности платежа.</typeparam>
    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        public PaymentService(IBaseRepository<Payment> repository) : base(repository)
        {
        }
    }

}
