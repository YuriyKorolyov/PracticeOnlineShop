using MyApp.Models;

namespace MyApp.Dto.ExportToExcel
{
    public class ViewHistoryExcelDto
    {
        /// <summary>
        /// Получает или задает идентификатор истории просмотров.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Получает или задает дату просмотра.
        /// </summary>
        public DateTime ViewDate { get; set; }

        /// <summary>
        /// Получает или задает пользователя, который просмотрел товар.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Получает или задает товар, который был просмотрен пользователем.
        /// </summary>
        public int ProductId { get; set; }
    }
}
