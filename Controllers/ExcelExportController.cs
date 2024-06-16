using Microsoft.AspNetCore.Mvc;
using MyApp.IServices;

namespace MyApp.Controllers
{
    public class ExcelExportController : ControllerBase
    {
        private readonly IExcelExportService _excelExportService;

        public ExcelExportController(IExcelExportService excelExportService)
        {
            _excelExportService = excelExportService;
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportAllTablesToExcel()
        {
            var fileContent = await _excelExportService.ExportAllTablesToExcelAsync();
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllTables.xlsx");
        }
    }
}
