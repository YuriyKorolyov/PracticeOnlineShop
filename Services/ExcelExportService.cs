using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.ExportToExcel;
using MyApp.IServices;
using MyApp.Models;
using MyApp.Repository.UnitOfWorks;

namespace MyApp.Services
{
    /// <summary>
    /// Сервис для экспорта данных из базы данных в Excel.
    /// </summary>
    public class ExcelExportService : IExcelExportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ExcelExportService"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of Work для работы с базой данных.</param>
        /// <param name="mapper">Mapper для маппинга сущностей в DTO.</param>
        /// <param name="roleManager">Менеджер ролей пользователей.</param>
        /// <param name="userManager">Менеджер пользователей.</param>
        public ExcelExportService(IUnitOfWork unitOfWork, IMapper mapper, RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _context = unitOfWork.GetContext();
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Экспортирует все таблицы базы данных в формате Excel.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Массив байтов Excel-файла.</returns>
        public async Task<byte[]> ExportAllTablesToExcelAsync(CancellationToken cancellationToken = default)
        {
            using (var workbook = new XLWorkbook())
            {
                await AddSheetWithMappingAsync<Cart, CartExcelDto>(workbook, "Carts", _context.Carts.Include(c => c.Product).Include(c => c.User), cancellationToken);
                await AddSheetWithMappingAsync<Category, CategoryExcelDto>(workbook, "Categories", _context.Categories, cancellationToken);
                await AddSheetWithMappingAsync<Order, OrderExcelDto>(workbook, "Orders", _context.Orders.Include(o => o.User), cancellationToken);
                await AddSheetWithMappingAsync<OrderDetail, OrderDetailExcelDto>(workbook, "OrderDetails", _context.OrderDetails.Include(od => od.Product).Include(od => od.Order), cancellationToken);
                await AddSheetWithMappingAsync<Payment, PaymentExcelDto>(workbook, "Payments", _context.Payments.Include(p => p.Order), cancellationToken);
                await AddSheetWithMappingAsync<Product, ProductExcelDto>(workbook, "Products", _context.Products, cancellationToken);
                await AddSheetWithMappingAsync<ProductCategory, ProductCategoryExcelDto>(workbook, "ProductCategories", _context.ProductCategories, cancellationToken);
                await AddSheetWithMappingAsync<PromoCode, PromoCodeExcelDto>(workbook, "PromoCodes", _context.PromoCodes, cancellationToken);
                await AddSheetWithMappingAsync<Review, ReviewExcelDto>(workbook, "Reviews", _context.Reviews.Include(r => r.User).Include(r => r.Product), cancellationToken);
                await AddSheetWithMappingAsync<User, UserExcelDto>(workbook, "Users", _context.Users, cancellationToken);
                await AddSheetWithMappingAsync<ViewHistory, ViewHistoryExcelDto>(workbook, "ViewHistories", _context.ViewHistories.Include(r => r.User).Include(r => r.Product), cancellationToken);
                
                await AddSheetWithoutMappingAsync(workbook, "AspNetRoles", _context.Roles, cancellationToken);

                await AddSheetUserRolesAsync(workbook, "UserRoles", _userManager, _roleManager, cancellationToken);

                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// Добавляет лист в Excel с маппингом сущностей на DTO.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TDto">Тип DTO.</typeparam>
        /// <param name="workbook">Excel-книга, в которую добавляется лист.</param>
        /// <param name="sheetName">Имя листа в Excel.</param>
        /// <param name="query">Запрос для получения данных из базы данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Асинхронную задачу для выполнения.</returns>
        private async Task AddSheetWithMappingAsync<TEntity, TDto>(XLWorkbook workbook, string sheetName, IQueryable<TEntity> query, CancellationToken cancellationToken = default)
            where TEntity : class
            where TDto : class
        {
            var worksheet = workbook.Worksheets.Add(sheetName);

            var mappedData = await query
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            if (mappedData.Count == 0)
            {
                worksheet.Cell(1, 1).Value = "No data available";
                return;
            }

            var properties = typeof(TDto).GetProperties();
            for (int j = 0; j < properties.Length; j++)
            {
                var cell = worksheet.Cell(1, j + 1);
                cell.Value = properties[j].Name;
                cell.Style.Font.Bold = true; 
            }

            for (int i = 0; i < mappedData.Count; i++)
            {
                var item = mappedData[i];
                for (int j = 0; j < properties.Length; j++)
                {
                    var value = properties[j].GetValue(item);
                    worksheet.Cell(i + 2, j + 1).Value = value?.ToString();
                }
            }

            worksheet.Columns().AdjustToContents();
        }

        /// <summary>
        /// Добавляет лист в Excel без маппинга сущностей на DTO.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="workbook">Excel-книга, в которую добавляется лист.</param>
        /// <param name="sheetName">Имя листа в Excel.</param>
        /// <param name="data">Данные для добавления в лист.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Асинхронную задачу для выполнения.</returns>
        private async Task AddSheetWithoutMappingAsync<TEntity>(IXLWorkbook workbook, string sheetName, IQueryable<TEntity> data, CancellationToken cancellationToken = default)
    where TEntity : class
        {
            var worksheet = workbook.Worksheets.Add(sheetName);
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var cell = worksheet.Cell(1, i + 1);
                cell.Value = properties[i].Name;
                cell.Style.Font.Bold = true;
            }

            var dataList = await data.ToListAsync(cancellationToken);

            for (int rowIndex = 0; rowIndex < dataList.Count; rowIndex++)
            {
                var rowData = dataList[rowIndex];
                for (int colIndex = 0; colIndex < properties.Length; colIndex++)
                {
                    var value = properties[colIndex].GetValue(rowData);
                    worksheet.Cell(rowIndex + 2, colIndex + 1).Value = value != null ? value.ToString() : string.Empty;
                }
            }
        }

        /// <summary>
        /// Добавляет лист в Excel с данными о ролях пользователей.
        /// </summary>
        /// <param name="workbook">Excel-книга, в которую добавляется лист.</param>
        /// <param name="sheetName">Имя листа в Excel.</param>
        /// <param name="userManager">Менеджер пользователей.</param>
        /// <param name="roleManager">Менеджер ролей пользователей.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Асинхронную задачу для выполнения.</returns>
        private async Task AddSheetUserRolesAsync(IXLWorkbook workbook, string sheetName, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, CancellationToken cancellationToken = default)
        {
            var worksheet = workbook.Worksheets.Add(sheetName);

            worksheet.Cell(1, 1).Value = "UserId";
            worksheet.Cell(1, 2).Value = "RoleId";

            var users = await userManager.Users.ToListAsync(cancellationToken);

            int rowIndex = 2;
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    var role = await roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        worksheet.Cell(rowIndex, 1).Value = user.Id;
                        worksheet.Cell(rowIndex, 2).Value = role.Id;
                        rowIndex++;
                    }
                }
            }
        }

    }
}
