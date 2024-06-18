using Microsoft.AspNetCore.Mvc;
using MyApp.IServices;

namespace MyApp.Controllers
{
    /// <summary>
    /// Контроллер для экспорта данных в Excel.
    /// </summary>
    public class ExcelExportController : ControllerBase
    {
        private readonly IExcelExportService _excelExportService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ExcelExportController"/>.
        /// </summary>
        /// <param name="excelExportService">Сервис для экспорта данных в Excel.</param>
        public ExcelExportController(IExcelExportService excelExportService)
        {
            _excelExportService = excelExportService;
        }

        /// <summary>
        /// Экспортирует все таблицы данных в Excel.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Файл Excel (.xlsx) с экспортированными данными.</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAllTablesToExcel(CancellationToken cancellationToken)
        {
            var fileContent = await _excelExportService.ExportAllTablesToExcelAsync(cancellationToken);
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllTables.xlsx");
        }
    }
}
