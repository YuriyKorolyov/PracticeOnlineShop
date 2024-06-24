namespace MyApp.Configurations
{
    /// <summary>
    /// Настройки для конфигурации JWT токенов.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Ключ для подписи JWT токенов.
        /// </summary>
        public string SigningKey { get; set; }

        /// <summary>
        /// Издатель JWT токенов.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Получатель JWT токенов.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Время жизни JWT токенов в днях.
        /// </summary>
        public int TokenLifetimeDays { get; set; }
    }
}
