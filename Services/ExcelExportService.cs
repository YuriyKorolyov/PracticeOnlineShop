using AutoMapper;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Dto.ExportToExcel;
using MyApp.IServices;
using MyApp.Models;

namespace MyApp.Services
{
    public class ExcelExportService : IExcelExportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExcelExportService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<byte[]> ExportAllTablesToExcelAsync()
        {
            using (var workbook = new XLWorkbook())
            {
                await AddSheetWithMappedDataAsync<Cart, CartExcelDto>(workbook, "Carts", _context.Carts.Include(c => c.Product).Include(c => c.User));
                await AddSheetWithMappedDataAsync<Category, CategoryExcelDto>(workbook, "Categories", _context.Categories);
                await AddSheetWithMappedDataAsync<Order, OrderExcelDto>(workbook, "Orders", _context.Orders.Include(o => o.User));
                await AddSheetWithMappedDataAsync<OrderDetail, OrderDetailExcelDto>(workbook, "OrderDetails", _context.OrderDetails.Include(od => od.Product).Include(od => od.Order));
                await AddSheetWithMappedDataAsync<Payment, PaymentExcelDto>(workbook, "Payments", _context.Payments.Include(p => p.Order));
                await AddSheetWithMappedDataAsync<Product, ProductExcelDto>(workbook, "Products", _context.Products);
                await AddSheetWithMappedDataAsync<ProductCategory, ProductCategoryExcelDto>(workbook, "ProductCategories", _context.ProductCategories);
                await AddSheetWithMappedDataAsync<PromoCode, PromoCodeExcelDto>(workbook, "PromoCodes", _context.PromoCodes);
                await AddSheetWithMappedDataAsync<Review, ReviewExcelDto>(workbook, "Reviews", _context.Reviews.Include(r => r.User).Include(r => r.Product));
                await AddSheetWithMappedDataAsync<Role, RoleExcelDto>(workbook, "Roles", _context.Roles);
                await AddSheetWithMappedDataAsync<User, UserExcelDto>(workbook, "Users", _context.Users.Include(u => u.Role));
                await AddSheetWithMappedDataAsync<ViewHistory, ViewHistoryExcelDto>(workbook, "ViewHistories", _context.ViewHistories.Include(r => r.User).Include(r => r.Product));

                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        private async Task AddSheetWithMappedDataAsync<TEntity, TDto>(XLWorkbook workbook, string sheetName, IQueryable<TEntity> query)
            where TEntity : class
            where TDto : class
        {
            var worksheet = workbook.Worksheets.Add(sheetName);
            var data = await query.ToListAsync();
            var mappedData = _mapper.Map<List<TDto>>(data);

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
    }
}
