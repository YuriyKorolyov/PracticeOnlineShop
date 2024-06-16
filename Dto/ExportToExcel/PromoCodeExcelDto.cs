using MyApp.Models;

namespace MyApp.Dto.ExportToExcel
{
    public class PromoCodeExcelDto
    {
        /// <summary>
        /// Получает или задает идентификатор промо-кода.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название промо-кода.
        /// </summary>
        public string PromoName { get; set; }

        /// <summary>
        /// Получает или задает скидку, предоставляемую промо-кодом.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Получает или задает дату начала действия промо-кода.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Получает или задает дату окончания действия промо-кода.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
