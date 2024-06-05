namespace MyApp.Dto.Read
{
    /// <summary>
    /// Представляет модель данных для чтения информации о промокоде.
    /// </summary>
    public class PromoCodeReadDto
    {
        /// <summary>
        /// Получает или задает идентификатор промокода.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название промокода.
        /// </summary>
        public string PromoName { get; set; }

        /// <summary>
        /// Получает или задает размер скидки, предоставляемой промокодом.
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
