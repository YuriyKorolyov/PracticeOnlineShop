using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Interfaces;
using MyApp.Models;
using MyApp.Repository.BASE;

namespace MyApp.Repository
{
    /// <summary>
    /// Репозиторий для управления операциями, связанными с платежами.
    /// </summary>
    /// <typeparam name="Payment">Тип сущности платежа.</typeparam>
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PaymentRepository"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных приложения.</param>
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
