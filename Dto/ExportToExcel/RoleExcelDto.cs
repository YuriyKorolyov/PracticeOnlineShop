using MyApp.Models;

namespace MyApp.Dto.ExportToExcel
{
    public class RoleExcelDto
    {
        /// <summary>
        /// Получает или задает идентификатор роли.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает название роли.
        /// </summary>
        public string RoleName { get; set; }
    }
}
