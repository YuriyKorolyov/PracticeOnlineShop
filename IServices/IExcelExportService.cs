using ClosedXML.Excel;

namespace MyApp.IServices
{
    public interface IExcelExportService
    {
        Task<byte[]> ExportAllTablesToExcelAsync();
    }
}
