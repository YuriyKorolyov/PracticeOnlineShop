namespace MyApp.Dto.Create
{
    /// <summary>
    /// Представляет модель данных для создания нового промокода.
    /// </summary>
    public class PromoCodeCreateDto
    {
        /// <summary>
        /// Получает или задает название промокода.
        /// </summary>
        public string PromoName { get; set; }

        /// <summary>
        /// Получает или задает размер скидки, предоставляемой промокодом (в долях от 0 до 1).
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Получает или задает дату начала действия промокода.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Получает или задает дату окончания действия промокода.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
