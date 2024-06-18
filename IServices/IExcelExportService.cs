namespace MyApp.IServices
{
    /// <summary>
    /// Интерфейс сервиса экспорта данных в Excel.
    /// </summary>
    public interface IExcelExportService
    {
        /// <summary>
        /// Асинхронно экспортирует все таблицы в Excel и возвращает содержимое файла в виде массива байтов.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены задачи.</param>
        /// <returns>Массив байтов, содержащий данные Excel файла.</returns>
        Task<byte[]> ExportAllTablesToExcelAsync(CancellationToken cancellationToken = default);
    }
}
