namespace MyApp.Dto.ExportToExcel
{
    /// <summary>
    /// DTO для экспорта данных пользователя в Excel.
    /// </summary>
    public class UserExcelDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ShippingAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
